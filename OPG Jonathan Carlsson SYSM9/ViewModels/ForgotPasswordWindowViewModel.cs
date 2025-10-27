using MVVM_KlonaMIg.MVVM;
using OPG_Jonathan_Carlsson_SYSM9.Managers;
using OPG_Jonathan_Carlsson_SYSM9.Models;
using OPG_Jonathan_Carlsson_SYSM9.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace OPG_Jonathan_Carlsson_SYSM9.ViewModels
{
    public class ForgotPasswordWindowViewModel : BaseViewModel
    {
        //OH NO, THE NAME OF THIS WINDOW GOES AGAINST THE NOOOORM (shouldn't have "window" in the name)!! i need to change this without destroying everything...
        //props
        private readonly UserManager _userManager;
        private readonly NavigationManager _navigationManager;

        private string _usernameInput;
        public string UsernameInput
        {
            get { return _usernameInput; }
            set
            {
                _usernameInput = value;
                OnPropertyChanged();
                //When the username is "set", the value for SecurityQuestion is also set through the method "FindUserSecurityQuestion"
                if (string.IsNullOrWhiteSpace(UsernameInput))
                {
                    SecurityQuestion = "Please enter your username!";
                }
                else
                {
                    SecurityQuestion = FindUserSecurityQuestionAndPassword();
                }
            }
        }
        private string _securityQuestion;
        public string SecurityQuestion
        {
            get { return _securityQuestion; }
            set
            {
                _securityQuestion = value;
                OnPropertyChanged();
            }
        }
        private string _questionAnswer;
        public string QuestionAnswer
        {
            get { return _questionAnswer; }
            set
            {
                _questionAnswer = value.ToLower();
                OnPropertyChanged();
            }
        }
        private bool _userFound;
        public bool UserFound
        {
            get { return _userFound; }
            set
            {
                _userFound = value;
                OnPropertyChanged();
            }
        }
        private string _usernamePassword;
        public string UsernamePassword
        {
            get { return _usernamePassword; }
            set
            {
                _usernamePassword = value;
                OnPropertyChanged();
            }
        }

        public ICommand GoBackCommand { get; }
        public ICommand SubmitCommand { get; }


        //construcor
        public ForgotPasswordWindowViewModel()
        {
            _userManager = (UserManager)Application.Current.Resources["UserManager"];
            _navigationManager = (NavigationManager)Application.Current.Resources["NavigationManager"];
            SubmitCommand = new RelayCommand(execute => ExecuteValidationSecurityQuestion(), canExecute => CanExecuteValidation());
            GoBackCommand = new RelayCommand(execute => ExecuteGoBack());

            SecurityQuestion = "Please enter your username!";
        }
        //methods
        private void ExecuteGoBack()
        {
            _navigationManager.CreateAndShowWindow<LoginWindow>();
            _navigationManager.CloseWindow<ForgotPasswordWindow>();
        }
        //Returns string value if the SecurityQuestion beloning to UsernameInput if the user exists.
        public string FindUserSecurityQuestionAndPassword()
        {
            foreach (User u in _userManager.Users)
            {
                if (u.Username == UsernameInput)
                {
                    UserFound = true;
                    UsernamePassword = u.Password;
                    return u.SecurityQuestion;
                }
            }
            UserFound = false;
            return "User not found, please check your username.";
        }
        private bool CanExecuteValidation()
        {
            return !string.IsNullOrWhiteSpace(UsernameInput) && !string.IsNullOrWhiteSpace(QuestionAnswer) && SecurityQuestion != "Please enter your username!"
                && SecurityQuestion != "User not found, please check your username.";
        }
        //Checks if the Answer for the SecurityQuestion is correct. If it's not, a messagebox will pop up, if it's correct, a window will pop up letting the user change password
        public void ExecuteValidationSecurityQuestion()
        {
                //Finds the correct answer and checks if it match with the input answer
                foreach (User u in _userManager.Users)
                {
                    if (u.Username == UsernameInput)
                    {
                        //Correct answer
                        if (u.SecurityAnswer == QuestionAnswer)
                        {
                        //Also logs in the user, making it easier for us to Change user password and such in the ChangePasswordWindow.
                            _userManager.LogIn(UsernameInput, UsernamePassword);
                            _navigationManager.CreateAndShowWindow<ChangePasswordWindow>();
                            _navigationManager.CloseWindow<ForgotPasswordWindow>();
                        }
                        //Incorrect Answer
                        else
                        {
                            MessageBox.Show("Incorrect answer, please try again", "Error");
                        }
                        return;
                    }
                }
        }
    }
}
