using MVVM_KlonaMIg.MVVM;
using OPG_Jonathan_Carlsson_SYSM9.Managers;
using OPG_Jonathan_Carlsson_SYSM9.Models;
using OPG_Jonathan_Carlsson_SYSM9.Views;
using OPG_Jonathan_Carlsson_SYSM9.Views.RecipeWindows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace OPG_Jonathan_Carlsson_SYSM9.ViewModels
{
    public class RecipeListWindowViewModel : BaseViewModel
    {
        //props
        private readonly RecipeManager _recipeManager;
        private readonly UserManager _userManager;
        private readonly NavigationManager _navigationManager;

        private User _loggedIn;
        public User LoggedIn
        {
            get { return _loggedIn; }
            set
            {
                _loggedIn = value;
                OnPropertyChanged();
            }
        }

        public string LoggedInUsername
        {
            get { return LoggedIn.Username; }
        }

        public string LoggedInRole
        {
            get
            {
                if (LoggedIn.IsAdmin)
                {
                    return "ADMIN";
                }
                else
                {
                    return "USER";
                }
            }
        }
        public int LoggedInID
        {
            get { return LoggedIn.Id; }
        }
        private Recipe _selectedRecipe;

        public Recipe SelectedRecipe
        {
            get { return _selectedRecipe; }
            set 
            {
                _selectedRecipe = value;
                OnPropertyChanged();
            }
        }


        //Will store the observable recipes for the specific logged in user OR all if they have an Admin role.
        public ObservableCollection<Recipe> Recipes { get; set; }

        //Lets the user Logout
        public ICommand LogoutCommand { get; }
        //Opens "AddRecipeWindow"
        public ICommand AddRecipeButtonCommand { get; }
        //Opens "RecipeDetailsWindow"
        public ICommand RecipeDetailsButton { get; }

        //Constructor

        public RecipeListWindowViewModel()
        {
            _recipeManager = (RecipeManager)Application.Current.Resources["RecipeManager"];
            _userManager = (UserManager)Application.Current.Resources["UserManager"];
            _navigationManager = (NavigationManager)Application.Current.Resources["NavigationManager"];
            LogoutCommand = new RelayCommand(execute => ExecuteLogout());
            AddRecipeButtonCommand = new RelayCommand(execute => ExecuteAddNewRecipeButton());
            RecipeDetailsButton = new RelayCommand(execute => ExecuteRecipeDetailsButton(), canExecute => CanExecuteRecipeDetailsButton());

            //Stores who's logged in into LoggedIn
            LoggedIn = _userManager.GetLoggedIn();
            LoadRecipes();

        }

        //Sends the recipes created by the logged in user to the ObservableCollection Recipes.
        //If the user is Admin, then all recipes are loaded.
        private void LoadRecipes()
        {
            if (LoggedIn.IsAdmin)
            {
                Recipes = new ObservableCollection<Recipe>(_recipeManager.GetAllRecipes());
            }
            else
            {
                Recipes = new ObservableCollection<Recipe>(_recipeManager.GetByUser(LoggedIn.Id));
            }
        }

        //Lets the user logout and return to the login window
        private void ExecuteLogout()
        {
            _userManager.LogOut();
            _navigationManager.CreateAndShowWindow<LoginWindow>();
            _navigationManager.CloseWindow<RecipeListWindow>();
        }
        //Lets the user enter the AddNewRecipe window
        private void ExecuteAddNewRecipeButton()
        {
            _navigationManager.CreateAndShowWindow<AddRecipeWindow>();
            _navigationManager.CloseWindow<RecipeListWindow>();
        }

        //Lets the user enter RecipeDetailsWindow, and also sends the selected recipe to a universal prop containing the selected recipe
        private void ExecuteRecipeDetailsButton()
        {
            _recipeManager.SelectedRecipe = SelectedRecipe;
            _navigationManager.CreateAndShowWindow<RecipeDetailsWindow>();
            _navigationManager.CloseWindow<RecipeListWindow>();
        }

        //Checks if the user is allowed to click "Recipe details".
        private bool CanExecuteRecipeDetailsButton()
        {
            if (SelectedRecipe != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
