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
        //prop for binding the command from LoginButton to LoginCommand
        public ICommand LoginCommand { get; }

        //constructor
        public LoginWindowViewModel(UserManager userManager)
        {
            _userManager = (UserManager)Application.Current.Resources["UserManager"];
            LoginCommand = new RelayCommand(ExecuteLogin, CanExecuteLogin);
        }

        //methods

        private void ExecuteLogin(object parameter)
        {
            Error = string.Empty;
            bool success = _userManager.LogIn(UsernameInput, PasswordInput);

            if (success)
            {
                //Opens RecipeListWindow
                RecipeListWindow recipeWindow = new RecipeListWindow();
                recipeWindow.Show();

                //Closes login window
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
        private bool CanExecuteLogin(Object parameter)
        {
            return !string.IsNullOrWhiteSpace(UsernameInput) && !string.IsNullOrWhiteSpace(PasswordInput);
        }
    }
}
