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
                    SecurityQuestion = FindUserSecurityQuestion();
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
                _questionAnswer = value;
                OnPropertyChanged();
            }
        }

        public ICommand CancelCommand { get; }


        //construcor
        public ForgotPasswordWindowViewModel()
        {
            _userManager = (UserManager)Application.Current.Resources["UserManager"];
            _navigationManager = (NavigationManager)Application.Current.Resources["NavigationManager"];
            CancelCommand = new RelayCommand(execute => ExecuteCancel());

            SecurityQuestion = "Please enter your username!";
        }
        //methods
        private void ExecuteCancel()
        {
            _navigationManager.ShowWindow<LoginWindow>();
            _navigationManager.CloseWindow<ForgotPasswordWindow>();
        }

        public string FindUserSecurityQuestion()
        {
            foreach (User u in _userManager.Users)
            {
                if (u.Username == UsernameInput)
                {
                    return u.SecurityQuestion;
                }
            }
            return "User not found, please check your username.";
        }
    }
}
