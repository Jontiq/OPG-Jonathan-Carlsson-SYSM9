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
    public class ChangePasswordViewModel : BaseViewModel
    {
        //props
        private readonly NavigationManager _navigationManager;
        private readonly UserManager _userManager;

        //normal props
        private User _loggedInUser;

        private string _loggedUsername;
        public string LoggedUsername
        {
            get { return _loggedUsername; }
            set
            {
                _loggedUsername = value;
                OnPropertyChanged();
            }
        }
        private string _newPassword;
        public string NewPassword
        {
            get { return _newPassword; }
            set
            {
                _newPassword = value;
                OnPropertyChanged();
            }
        }
        private string _confirmPassword;
        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set
            {
                _confirmPassword = value;
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

        public ICommand GoBackCommand { get; }

        //constructor
        public ChangePasswordViewModel()
        {
            //_navigationManager = (NavigationManager)Application.Current.Resources["NavigationManager"];
            _navigationManager = (NavigationManager)Application.Current.Resources["NavigationManager"];
            _userManager = (UserManager)Application.Current.Resources["UserManager"];
            GoBackCommand = new RelayCommand(execute => ExecuteGoBack());

            //Gets the logged in user and sends the user values to designated prop
            LoggedUsername = _userManager.GetLoggedIn().Username;
        }

        //methods
        //Checks if all of the password requirements are met before showing the Update Password button.
        private bool CanExecuteUpdatePassword()
        {
            return HasNumberAndSpecial && IsLengthValid && PasswordsMatch;
        }
        //Updates the password
        public void ExecuteUpdatePassword()
        {
            MessageBox.Show("Password has been updated!");
            _userManager.ChangePassword(NewPassword);
            //logs out for just in case
            _userManager.LogOut();
            //Sends the user back to the login window
            _navigationManager.CreateAndShowWindow<LoginWindow>();
            _navigationManager.CloseWindow<ChangePasswordWindow>();
        }

        //If the user presses "Go back", they are "logged out" and returned to the previous page, different from pressing "x" which would be the method "ExecuteCancel"
        private void ExecuteGoBack()
        {
            //Also logs out the user when the window is closed, just a safety measure.
            _userManager.LogOut();
            _navigationManager.CreateAndShowWindow<ForgotPasswordWindow>();
            _navigationManager.CloseWindow<ChangePasswordWindow>();
        }

        public void CheckPasswordRules()
        {
            //Checks if the password is 8 characters or more
            if (!string.IsNullOrEmpty(NewPassword) && NewPassword.Length >= 8)
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
            if (!string.IsNullOrWhiteSpace(NewPassword))
            {
                foreach (char c in NewPassword)
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

            if (!string.IsNullOrWhiteSpace(NewPassword) && NewPassword == ConfirmPassword)
            {
                PasswordsMatch = true;
            }
            else
            {
                PasswordsMatch = false;
            }

        }
    }
}
