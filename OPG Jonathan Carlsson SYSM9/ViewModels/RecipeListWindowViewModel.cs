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



        //These are the recipes that the logged in user has access to.
        //If a normal user, only the user created recipes will be stored. If an admin, all recipes will be stored.
        private ObservableCollection<Recipe> _accessRecipes;
        public ObservableCollection<Recipe> AccessRecipes
        {
            get { return _accessRecipes; }
            set
            {
                _accessRecipes = value;
                OnPropertyChanged();
            }
        }

        //These are recipes shown in the datagrid. This will be manipulated by the filter.
        private ObservableCollection<Recipe> _shownRecipes;
        public ObservableCollection<Recipe> ShownRecipes
        {
            get { return _shownRecipes; }
            set
            {
                _shownRecipes = value;
                OnPropertyChanged();
            }
        }
        //Acts as the filter field value
        private string _filterText;
        public string FilterText
        {
            get { return _filterText; }
            set
            {
                _filterText = value;
                OnPropertyChanged();
                //Everytime the filter value is set, the "ApplyFilter" method is called.
                ApplyFilter();
            }
        }

        //Lets the user Logout
        public ICommand LogoutCommand { get; }
        //Opens "AddRecipeWindow"
        public ICommand AddRecipeButtonCommand { get; }
        //Opens "RecipeDetailsWindow"
        public ICommand RecipeDetailsButton { get; }
        //Opens the information window
        public ICommand InformationCommand { get; }
        //Opens the user details window
        public ICommand UserDetailsCommand { get; }
        //Lets the user remove a selected recipe
        public ICommand DeleteRecipeCommand { get; }

        //Constructor

        public RecipeListWindowViewModel()
        {
            _recipeManager = (RecipeManager)Application.Current.Resources["RecipeManager"];
            _userManager = (UserManager)Application.Current.Resources["UserManager"];
            _navigationManager = (NavigationManager)Application.Current.Resources["NavigationManager"];
            LogoutCommand = new RelayCommand(execute => ExecuteLogout());
            AddRecipeButtonCommand = new RelayCommand(execute => ExecuteAddNewRecipeButton());
            RecipeDetailsButton = new RelayCommand(execute => ExecuteRecipeDetailsButton(), canExecute => RecipeSelected());
            InformationCommand = new RelayCommand(execute => ExecuteInformation());
            UserDetailsCommand = new RelayCommand(execute => ExecuteUserDetails());
            DeleteRecipeCommand = new RelayCommand(execute => ExecuteRemoveRecipe(), canExecute => RecipeSelected());

            //Stores who's logged in into LoggedIn
            LoggedIn = _userManager.GetLoggedIn();
            LoadRecipes();

        }

        //Sends the recipes created by the logged in user to the ObservableCollection Recipes.
        //If the user is Admin, then all recipes are loaded.
        private void LoadRecipes()
        {
            //If the logged-in user is an Admin, load ALL recipes.
            if (LoggedIn.IsAdmin)
            {
                AccessRecipes = new ObservableCollection<Recipe>(_recipeManager.Recipes);
            }
            //If the user is not Admin, only load recipes created by that user.
            else
            {
                //Creates empty collection
                ObservableCollection<Recipe> userRecipes = new ObservableCollection<Recipe>();
                //Goes through all recipes
                foreach (Recipe r in _recipeManager.Recipes)
                {
                    //If the recipe createdByID matches the currently logged in user, then it will add that recipe to "userRecipes"
                    if (r.CreatedByID == LoggedIn.Id)
                    {
                        userRecipes.Add(r);
                    }
                }
                //AccessRecipes now mirror the userRecipes
                AccessRecipes = userRecipes;
            }
            //At startup, ShownRecipes should mirror AccessRecipes now, meaning everything the user has access to is initially displayed.
            ShownRecipes = new ObservableCollection<Recipe>(AccessRecipes);
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
        private bool RecipeSelected()
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

        //Lets the user open the cookmaster information windwow
        private void ExecuteInformation()
        {
            _navigationManager.CreateAndShowWindow<InfoWindow>();
            _navigationManager.CloseWindow<RecipeListWindow>();
        }

        //Removes recipe
        private void ExecuteRemoveRecipe()
        {
            var result = MessageBox.Show("Are you sure you want to delete this recipe?", "Confirm Deletion", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            //Removes the recipe bu the recipe ID reference from the Recipe list in RecipeManager and in the datagrid view.
            if (result == MessageBoxResult.Yes)
            {
                //Removes the selected recipe from Recipes list in RecipeManager using the Recipe ID.
                for (int i = 0; i < _recipeManager.Recipes.Count; i++)
                {
                    if (_recipeManager.Recipes[i].Id == SelectedRecipe.Id)
                    {
                        _recipeManager.Recipes.RemoveAt(i);
                    }
                }
                //Removes the selected recipe from AccessRecipes using the Recipe ID.
                for (int i = AccessRecipes.Count - 1; i >= 0; i--)
                {
                    if (AccessRecipes[i].Id == SelectedRecipe.Id)
                    {
                        AccessRecipes.RemoveAt(i);
                    }
                }
                //Removes the selected recipe from ShownRecipes using the Recipe ID.
                for (int i = ShownRecipes.Count - 1; i >= 0; i--)
                {
                    if (ShownRecipes[i].Id == SelectedRecipe.Id)
                    {
                        ShownRecipes.RemoveAt(i);
                    }
                }
            }
        }

        //Opens "User details"
        public void ExecuteUserDetails()
        {
            _navigationManager.CreateAndShowWindow<UserDetailsWindow>();
            _navigationManager.CloseWindow<RecipeListWindow>();
        }

        //Changes the shown recipes in "ShownRecipes" depening on the value set in filter.
        private void ApplyFilter()
        {
            //If the filter field is empty, All the recipes the user has access to is shown
            if (string.IsNullOrWhiteSpace(FilterText))
            {
                //Clears the list (Maybe this works instead of making new list everytime its empty instead?)
                ShownRecipes.Clear();

                //Copy all recipes from AccessRecipes
                foreach (Recipe r in AccessRecipes)
                {
                    ShownRecipes.Add(r);
                }

                return;
            }

            //Converts the filtertext to lowercase for better matchability
            string filter = FilterText.ToLower();
            //Clears list again, since we will fill it later with what matches with the filter.
            ShownRecipes.Clear();


            foreach (Recipe r in AccessRecipes)
            {
                //sets all the "searchable" fields to lowercase, also for matchabilty
                string title = r.Title.ToLower();
                string category = r.Category.ToLower();
                //converts the date to string value. I also use "MM" for months, otehrwise for some reason it would mean minutes
                string date = r.Date.ToString("yyyy-MM-dd");

                //If any of the recipes fields match (contains the filter value), it is added to our result list
                if (title.Contains(filter) || category.Contains(filter) || date.Contains(filter))
                {
                    ShownRecipes.Add(r);
                }
            }
        }
    }
}
