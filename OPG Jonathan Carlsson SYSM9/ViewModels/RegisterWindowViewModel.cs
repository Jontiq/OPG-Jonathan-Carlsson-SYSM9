using MVVM_KlonaMIg.MVVM;
using OPG_Jonathan_Carlsson_SYSM9.Managers;
using OPG_Jonathan_Carlsson_SYSM9.Models;
using OPG_Jonathan_Carlsson_SYSM9.Views;
using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace OPG_Jonathan_Carlsson_SYSM9.ViewModels
{
    public class RegisterWindowViewModel : BaseViewModel
    {
        //props
        //Refrences to globally shared singletons
        //Private readonly just because we don't want it to be able to change outside of class, and only to be "created" in the constructor
        private readonly UserManager _userManager;
        private readonly NavigationManager _navigationManager;

        //"Normal" props
        private string _usernameInput;
        public string UsernameInput
        {
            get { return _usernameInput; }
            set
            {
                _usernameInput = value;
                OnPropertyChanged();
                //Checks if the username is taken everytime the username is updated, and sets the boolean "UsernameTaken" to true och false.
                CheckIfUsernameTaken();
            }
        }
        public string[] Countries { get; } =
        {
                "Sweden",
                "Norway",
                "Finland",
                "Denmark",
                "Iceland"
        };
        private string _selectedCountry;
        public string SelectedCountry
        {
            get { return _selectedCountry; }
            set
            {
                _selectedCountry = value;
                OnPropertyChanged();
            }
        }
        public string[] Questions { get; } =
        {
                "What city were you born in?",
                "What was the first concert you attended?",
                "In what city did your parents meet?",
                "What was the name of your first pet?",
        };
        private string _selectedQuestion;
        public string SelectedQuestion
        {
            get { return _selectedQuestion; }
            set
            {
                _selectedQuestion = value;
                OnPropertyChanged();
            }
        }
        private string _questionAnswer;
        public string QuestionAnswer
        {
            get { return _questionAnswer; }
            set
            {
                _questionAnswer = value;
                OnPropertyChanged();
            }
        }
        private string _passwordInput;
        public string PasswordInput
        {
            get { return _passwordInput; }
            set
            {
                _passwordInput = value;
                OnPropertyChanged();
            }
        }
        private string _confirmPwInput;
        public string ConfirmPwInput
        {
            get { return _confirmPwInput; }
            set
            {
                _confirmPwInput = value;
                OnPropertyChanged();
            }
        }
        private bool _isLengthValid;
        public bool IsLengthValid
        {
            get { return _isLengthValid; }
            set
            {
                _isLengthValid = value;
                OnPropertyChanged();
            }
        }
        private bool _hasNumberAndSpecial;
        public bool HasNumberAndSpecial
        {
            get { return _hasNumberAndSpecial; }
            set
            {
                _hasNumberAndSpecial = value;
                OnPropertyChanged();
            }
        }
        private bool _passwordsMatch;
        public bool PasswordsMatch
        {
            get { return _passwordsMatch; }
            set
            {
                _passwordsMatch = value;
                OnPropertyChanged();
            }
        }
        private bool _usernameTaken;
        public bool UsernameTaken
        {
            get { return _usernameTaken; }
            set
            {
                _usernameTaken = value;
                OnPropertyChanged();
            }
        }

        //Command props
        public ICommand CancelCommand { get; }
        public ICommand RegisterUserCommand { get; }

        //constructor
        public RegisterWindowViewModel()
        {
            _userManager = (UserManager)Application.Current.Resources["UserManager"];
            _navigationManager = (NavigationManager)Application.Current.Resources["NavigationManager"];
            //LoginCommand = new RelayCommand(execute => ExecuteLogin(), canExecute => CanExecuteLogin());
            RegisterUserCommand = new RelayCommand(execute => ExecuteCreateUser(), canExecute => CanExecuteCreateUser());
            CancelCommand = new RelayCommand(execute => ExecuteCancel());

        }

        //methods
        private void ExecuteCreateUser()
        {
            _userManager.Users.Add(new User(UsernameInput, PasswordInput, SelectedCountry, SelectedQuestion, QuestionAnswer.ToLower()));
            MessageBox.Show("New user has been created!");
            _navigationManager.ShowWindow<LoginWindow>();
            _navigationManager.CloseWindow<RegisterWindow>();
        }

        private bool CanExecuteCreateUser()
        {
            return HasNumberAndSpecial && IsLengthValid && PasswordsMatch && !UsernameTaken && SelectedCountry != null && SelectedQuestion != null && !string.IsNullOrWhiteSpace(QuestionAnswer);
        }
        //Checks if all the password requirements are followed
        public void CheckPasswordRules()
        {
            //Checks if the password is 8 characters or more
            if (!string.IsNullOrEmpty(PasswordInput) && PasswordInput.Length >= 8)
            {
                IsLengthValid = true;
            }
            else
            {
                IsLengthValid = false;
            }

            //Checks if the password contains atleas 1 number and special character
            bool hasDigit = false;
            bool hasSpecial = false;
            //IsNullOrWhiteSpace because of char control, since 1 space is considered whitespcace
            if (!string.IsNullOrWhiteSpace(PasswordInput))
            {
                foreach (char c in PasswordInput)
                {
                    if (char.IsDigit(c))
                    {
                        hasDigit = true;
                    }
                    else if (!char.IsLetterOrDigit(c))
                    {
                        hasSpecial = true;
                    }
                }
            }
            if (hasSpecial && hasDigit)
            {
                HasNumberAndSpecial = true;
            }
            else
            {
                HasNumberAndSpecial = false;
            }

            if(!string.IsNullOrWhiteSpace(PasswordInput) && PasswordInput == ConfirmPwInput)
            {
                PasswordsMatch = true;
            }
            else
            {
                PasswordsMatch = false;
            }

        }

        public void CheckIfUsernameTaken()
        {
            bool found = false;
            foreach (User user in _userManager.Users)
            {
                if (UsernameInput == user.Username)
                {
                    found = true;
                    break;
                }
                else
                {
                    found = false;
                }
            }
            UsernameTaken = found;
        }

        //Cancels the registration, opens the LoginWindow and closes the RegisterWindow without saving anything.
        private void ExecuteCancel()
        {
            _navigationManager.ShowWindow<LoginWindow>();
            _navigationManager.CloseWindow<RegisterWindow>();
        }

    }
}
