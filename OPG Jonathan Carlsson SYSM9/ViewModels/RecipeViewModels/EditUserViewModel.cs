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
    public class EditUserViewModel : BaseViewModel
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
                //Checks if the username is less than 4 characters, and sets the boolean "UsernameLessThan4" to true och false.
                CheckIfUsernameLessThan4();
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
        private bool _usernameLessThan4;
        public bool UsernameLessThan4
        {
            get { return _usernameLessThan4; }
            set
            {
                _usernameLessThan4 = value;
                OnPropertyChanged();
            }
        }

        public ICommand GoBackCommand { get; }
        public ICommand SaveUserCommand { get; }

        //constructor
        public EditUserViewModel()
        {
            _userManager = (UserManager)Application.Current.Resources["UserManager"];
            _navigationManager = (NavigationManager)Application.Current.Resources["NavigationManager"];
            GoBackCommand = new RelayCommand(execute => ExecuteGoBack());
            SaveUserCommand = new RelayCommand(execute => ExecuteSaveUser(),canExecute => CanExecuteSaveUser());

            LoggedInUser = _userManager.GetLoggedIn();

            //pre-fills the username input to be the old username for clarity
            UsernameInput = LoggedInUser.Username;

        }

        //methods

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

            if (!string.IsNullOrWhiteSpace(PasswordInput) && PasswordInput == ConfirmPwInput)
            {
                PasswordsMatch = true;
            }
            else
            {
                PasswordsMatch = false;
            }

        }
        //Checks if the username is taken
        public void CheckIfUsernameTaken()
        {
            bool found = false;

            //Lets the user keep their username if they want. Otherwise error "username is taken" would be shown.
            if (UsernameInput == LoggedInUser.Username)
            {
                found = false;
                return;
            }

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

        public void CheckIfUsernameLessThan4()
        {
            bool lessThan4 = UsernameInput.Trim().Length < 4;
            UsernameLessThan4 = lessThan4;
        }
        //Cancels the edit, opens the RecipeListWindow and closes the EditUser without saving anything.
        private void ExecuteGoBack()
        {
            MessageBox.Show("Changes has not been saved.");
            _navigationManager.CreateAndShowWindow<RecipeListWindow>();
            _navigationManager.CloseWindow<EditUserWindow>();
        }

        private bool CanExecuteSaveUser()
        {
            //Return false if username is taken or too short
            if (UsernameTaken || UsernameLessThan4)
                return false;

            // Return false if password rules fail
            if (!IsLengthValid || !HasNumberAndSpecial || !PasswordsMatch)
                return false;

            // Return false if country not selected
            if (string.IsNullOrWhiteSpace(SelectedCountry))
                return false;

            // Otherwise OK
            return true;
        }

        private void ExecuteSaveUser()
        {
            MessageBoxResult result = MessageBox.Show(
               "Are you sure you want to save these changes?",
               "Confirm changes",
               MessageBoxButton.YesNo,
               MessageBoxImage.Question
           );

            if (result == MessageBoxResult.Yes)
            {
                LoggedInUser.Username = UsernameInput;
                LoggedInUser.Password = PasswordInput;
                LoggedInUser.Country = SelectedCountry;

                _userManager.UpdateUser(LoggedInUser);
                MessageBox.Show("Changes have been saved successfully.");
                _navigationManager.CreateAndShowWindow<RecipeListWindow>();
                _navigationManager.CloseWindow<EditUserWindow>();
            }
        }

    }
}
