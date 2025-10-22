using MVVM_KlonaMIg.MVVM;
using OPG_Jonathan_Carlsson_SYSM9.Managers;
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
    public class LoginWindowViewModel : BaseViewModel
    {
        //props
        //private read only just because we don't want it to be able to change outside of class, and only to be "created" in the constructor
        private readonly UserManager _userManager;
        private string _usernameInput;
        public string UsernameInput
        {
            get { return _usernameInput; }
            set
            {
                _usernameInput = value;
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
        //prop for error message in the UI if the user writes invalid password, invalid username etc.
        private string _error;
        public string Error
        {
            get { return _error; }
            set
            {
                _error = value;
                OnPropertyChanged();
            }
        }
        //command props
        public ICommand LoginCommand { get; }
        //the two below is not in use yet
        public ICommand RegisterCommand { get; }
        public ICommand ForgotPasswordCommand { get; }

        //constructor
        public LoginWindowViewModel()
        {
            _userManager = (UserManager)Application.Current.Resources["UserManager"];
            LoginCommand = new RelayCommand(execute => ExecuteLogin(), canExecute => CanExecuteLogin());
        }

        //methods
        //Login method
        private void ExecuteLogin()
        {
            Error = string.Empty;
            bool success = _userManager.LogIn(UsernameInput, PasswordInput);

            if (success)
            {
                //Opens RecipeListWindow (Currently empty)
                RecipeListWindow recipeWindow = new RecipeListWindow();
                recipeWindow.Show();

                //Closes login window (perhaps this should be changed? Ineffective?)
                foreach(Window window in Application.Current.Windows)
                {
                    if (window is LoginWindow)
                    {
                        window.Close();
                        break;
                    }
                }
            }
            else
            {
                //Will be seen on the loginwindow and presented to user in textblock
                Error = "Invalid username or password.";
            }
        }

        //Helps us gray oout the login button, meaning that both fields in the login UI must have data.
        private bool CanExecuteLogin()
        {
            return !string.IsNullOrWhiteSpace(UsernameInput) && !string.IsNullOrWhiteSpace(PasswordInput);
        }
    }
}
