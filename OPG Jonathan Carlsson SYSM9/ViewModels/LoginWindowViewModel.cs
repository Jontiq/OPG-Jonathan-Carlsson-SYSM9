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
                if (!string.IsNullOrEmpty(Error))
                {
                    Error = string.Empty;
                }
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
                if (!string.IsNullOrEmpty(Error))
                {
                    Error = string.Empty;
                }
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
        //Command props
        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }
        //The command below is not in use yet
        public ICommand ForgotPasswordCommand { get; }

        //constructor
        public LoginWindowViewModel()
        {
            _userManager = (UserManager)Application.Current.Resources["UserManager"];
            _navigationManager = (NavigationManager)Application.Current.Resources["NavigationManager"];
            LoginCommand = new RelayCommand(execute => ExecuteLogin(), canExecute => CanExecuteLogin());
            RegisterCommand = new RelayCommand(execute => ExecuteRegister());
            ForgotPasswordCommand = new RelayCommand(execute => ExecuteForgotPassword());
        }

        //methods
        //Calls the LogIn method from UserManager and if succesful, opens the RecipeListWindow and CLOSES the LoginWindow
        private void ExecuteLogin()
        {
            Error = string.Empty;
            bool success = _userManager.LogIn(UsernameInput, PasswordInput);

            if (success)
            {
                _navigationManager.CreateAndShowWindow<TwoFactorAuthWindow>();
                _navigationManager.CloseWindow<LoginWindow>();
            }
            else
            {
                //Will be seen on the loginwindow and presented to user in textblock
                Error = "Invalid username or password.";
            }
        }
        //Helps us gray out the login button, meaning that both fields in the login UI must have data
        private bool CanExecuteLogin()
        {
            return !string.IsNullOrWhiteSpace(UsernameInput) && !string.IsNullOrWhiteSpace(PasswordInput) /*&&*/ /*PasswordInput.Length >= 8*/;
        }
        //Opens RegisterWindow and HIDES LoginWindow
        private void ExecuteRegister()
        {
            _navigationManager.CreateAndShowWindow<RegisterWindow>();
            _navigationManager.CloseWindow<LoginWindow>();
        }
        //Opens ForgotPasswordWindow and HIDES LoginWindow
        private void ExecuteForgotPassword()
        {
            _navigationManager.CreateAndShowWindow<ForgotPasswordWindow>();
            _navigationManager.CloseWindow<LoginWindow>();
        }
    }
}
