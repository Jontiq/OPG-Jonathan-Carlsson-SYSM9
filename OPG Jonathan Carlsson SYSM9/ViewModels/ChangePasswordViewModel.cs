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

        //constructor
        public ChangePasswordViewModel()
        {
            //_navigationManager = (NavigationManager)Application.Current.Resources["NavigationManager"];
            _navigationManager = (NavigationManager)Application.Current.Resources["NavigationManager"];
            _userManager = (UserManager)Application.Current.Resources["UserManager"];
            CancelCommand = new RelayCommand(execute => ExecuteCancel());

            //Gets the logged in user and sends the user values to designated prop
            User LoggedInUser = _userManager.GetLoggedIn();
            LoggedUsername = LoggedInUser.Username;
        }
        private void ExecuteCancel()
        {
            _navigationManager.ShowWindow<LoginWindow>();
            _navigationManager.CloseWindow<ForgotPasswordWindow>();
        }
    }
}
