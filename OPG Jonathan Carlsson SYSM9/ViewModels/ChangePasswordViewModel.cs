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

        public ICommand CancelCommand { get; }
        public ICommand GoBackCommand { get; }

        //constructor
        public ChangePasswordViewModel()
        {
            //_navigationManager = (NavigationManager)Application.Current.Resources["NavigationManager"];
            _navigationManager = (NavigationManager)Application.Current.Resources["NavigationManager"];
            _userManager = (UserManager)Application.Current.Resources["UserManager"];
            CancelCommand = new RelayCommand(execute => ExecuteCancel());
            GoBackCommand = new RelayCommand(execute => ExecuteGoBack());

            //Gets the logged in user and sends the user values to designated prop
            User LoggedInUser = _userManager.GetLoggedIn();
            LoggedUsername = LoggedInUser.Username;
        }
        private void ExecuteCancel()
        {
            //Also logs out the user when the window is closed, just a safety measure.
            _userManager.LogOut();
            _navigationManager.ShowWindow<LoginWindow>();
            _navigationManager.CloseWindow<ChangePasswordWindow>();

        }
        //If the user presses "Go back", they are "logged out" and returned to the previous page, different from pressing "x" which would be the method "ExecuteCancel"
        private void ExecuteGoBack()
        {
            //Also logs out the user when the window is closed, just a safety measure.
            _userManager.LogOut();
            _navigationManager.ShowWindow<ForgotPasswordWindow>();
            _navigationManager.CloseWindow<ChangePasswordWindow>();
        }
    }
}
