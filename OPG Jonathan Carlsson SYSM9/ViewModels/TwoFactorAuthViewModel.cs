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
    public class TwoFactorAuthViewModel : BaseViewModel
    {
        private readonly UserManager _userManager;
        private readonly NavigationManager _navigationManager;

        private string _enteredCode;
        public string EnteredCode
        {
            get { return _enteredCode; }
            set
            {
                _enteredCode = value;
                OnPropertyChanged();
            }
        }

        private string _generatedCode;

        public ICommand ConfirmCodeCommand { get; }

        public TwoFactorAuthViewModel()
        {
            _navigationManager = (NavigationManager)Application.Current.Resources["NavigationManager"];
            _userManager = (UserManager)Application.Current.Resources["UserManager"];

            //Creates the "random" code, 6 digits for the user and converts it to string and stores it in _generatedCode for matching later
            Random random = new Random();
            _generatedCode = random.Next(100000, 999999).ToString();

            //Simulate "EMAIL" and shows the user the code :)
            MessageBox.Show($"Your verification code is: {_generatedCode}", "Two-Factor Authentication");

            ConfirmCodeCommand = new RelayCommand(execute => ExecuteConfirmCode());
        }

        private void ExecuteConfirmCode()
        {
            //If the code is correct, the recipelist will be shown
            if (EnteredCode == _generatedCode)
            {
                MessageBox.Show("Login successful!");
                _navigationManager.CreateAndShowWindow<RecipeListWindow>();
                _navigationManager.CloseWindow<TwoFactorAuthWindow>();
            }
            //if the code is incorrect, the user gets logged out again and is sent back to login window
            else
            {
                MessageBox.Show("Incorrect code. Login failed.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                _userManager.LogOut();
                _navigationManager.CreateAndShowWindow<LoginWindow>(); // tillbaka till login
                _navigationManager.CloseWindow<TwoFactorAuthWindow>(); // stänger 2FA-fönstret
            }
        }
    }
}
