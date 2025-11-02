using MVVM_KlonaMIg.MVVM;
using OPG_Jonathan_Carlsson_SYSM9.Managers;
using OPG_Jonathan_Carlsson_SYSM9.Models;
using OPG_Jonathan_Carlsson_SYSM9.Views;
using OPG_Jonathan_Carlsson_SYSM9.Views.RecipeWindows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace OPG_Jonathan_Carlsson_SYSM9.ViewModels.RecipeViewModels
{
    public class UserDetailsViewModel : BaseViewModel
    {
        //props
        private readonly UserManager _userManager;
        private readonly NavigationManager _navigationManager;

        
        private User _loggedInUser;
        public User LoggedInUser
        {
            get { return _loggedInUser; }
            set 
            {
                _loggedInUser = value;
                OnPropertyChanged();
            }
        }

        private string _username;
        public string Username
        {
            get { return LoggedInUser.Username; }
        }

        private string _country;
        public string Country
        {
            get { return LoggedInUser.Country; }
        }

        private string _role;
        public string Role
        {
            get 
            {
                if (LoggedInUser.IsAdmin)
                {
                    return "ADMIN";
                }
                else
                {
                    return "USER";
                }
            }
        }

        private string _userID;
        public string UserID
        {
            get { return LoggedInUser.Id.ToString(); }
        }


        //Sends the user back to RecipeList window
        public ICommand GoBackCommand { get; }

        //constructor
        public UserDetailsViewModel()
        {
            _userManager = (UserManager)Application.Current.Resources["UserManager"];
            _navigationManager = (NavigationManager)Application.Current.Resources["NavigationManager"];

            LoggedInUser = _userManager.GetLoggedIn();

            GoBackCommand = new RelayCommand(execute => ExecuteGoBack());
        }

        //methods

        //Sends the user back to the RecipeListWindow
        private void ExecuteGoBack()
        {
            _navigationManager.CreateAndShowWindow<RecipeListWindow>();
            _navigationManager.CloseWindow<UserDetailsWindow>();
        }

        //Lets the user edit the user info.
        private void ExecuteEditUser()
        {

        }

    }
}
