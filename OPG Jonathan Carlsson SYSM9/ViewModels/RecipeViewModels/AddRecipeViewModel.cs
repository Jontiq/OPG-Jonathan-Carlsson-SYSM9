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

namespace OPG_Jonathan_Carlsson_SYSM9.ViewModels.RecipeViewModels
{
    public class AddRecipeViewModel : BaseViewModel
    {
        //props
        private readonly RecipeManager _recipeManager;
        private readonly UserManager _userManager;
        private readonly NavigationManager _navigationManager;

        private string _title;
        public string Title
        {
            get { return _title; }
            set 
            {
                _title = value;
                OnPropertyChanged();
            }
        }
        //Ingredients being an observable collection since the user may add or remove ingredients before registering.
        private string _ingredients;
        public string Ingredients
        {
            get { return _ingredients; }
            set 
            { 
                _ingredients = value;
                OnPropertyChanged();
            }
        }
        private string _instructions;
        public string Instructions
        {
            get { return _instructions; }
            set
            {
                _instructions = value;
                OnPropertyChanged();
            }
        }
        //A drop down list of pre-made categories for the user to pick from.
        //Readonly because we only want to collect the data here
        private readonly List<string> _categories = new List<string>
            {
                "Breakfast",
                "Lunch",
                "Dinner",
                "Dessert",
                "Snack",
                "Vegetarian",
                "Vegan",
            };
        public List<string> Categories
        {
            get { return _categories; }
        }

        private string _selectedCategory;
        public string SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                _selectedCategory = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddNewRecipeCommand { get; }
        //Lets the user cancel registrating new recipe and goes back to RecipeListWindow
        public ICommand GoBackCommand { get; }


        public AddRecipeViewModel()
        {
            _recipeManager = (RecipeManager)Application.Current.Resources["RecipeManager"];
            _userManager = (UserManager)Application.Current.Resources["UserManager"];
            _navigationManager = (NavigationManager)Application.Current.Resources["NavigationManager"];

            AddNewRecipeCommand = new RelayCommand(execute => ExecuteNewRecipe(), canExecute => CanExecuteNewRecipe());
            GoBackCommand = new RelayCommand(execute => ExecuteGoBack());

        }

        //Constructor for a copied recipe
        public AddRecipeViewModel(string title, string ingredients, string instructions, string selectedCategory)
        {
            _recipeManager = (RecipeManager)Application.Current.Resources["RecipeManager"];
            _userManager = (UserManager)Application.Current.Resources["UserManager"];
            _navigationManager = (NavigationManager)Application.Current.Resources["NavigationManager"];

            AddNewRecipeCommand = new RelayCommand(execute => ExecuteNewRecipe(), canExecute => CanExecuteNewRecipe());
            GoBackCommand = new RelayCommand(execute => ExecuteGoBack());

            //Assigns the values from the copied recipe
            Title = title;
            Ingredients = ingredients;
            Instructions = instructions;
            SelectedCategory = selectedCategory;
        }

        //Adds new recipe
        private void ExecuteNewRecipe()
        {
            _recipeManager.AddRecipe(new Recipe(
                0, 
                Title, 
                //Since Ingredients is a list in the method but an observablecollection here, we need to convert :)
                Ingredients, 
                Instructions, 
                SelectedCategory, 
                //Default since this will enter the current date when the user is registering the recipe
                default, 
                //Fetches the logged in user ID
                _userManager.GetLoggedIn().Id));

            MessageBox.Show("New recipe has been added!");
            _navigationManager.CreateAndShowWindow<RecipeListWindow>();
            _navigationManager.CloseWindow<AddRecipeWindow>();
        }

        private bool CanExecuteNewRecipe()
        {
            //Checks title
            if (string.IsNullOrWhiteSpace(Title))
                return false;
            //Ingredients is not empty
            if (string.IsNullOrWhiteSpace(Ingredients))
                return false;
            //There must be instructions
            if (string.IsNullOrWhiteSpace(Instructions))
                return false;
            //No missing categories
            if (string.IsNullOrWhiteSpace(SelectedCategory))
                return false;
            //If all is good
            return true;
        }
        private void ExecuteGoBack()
        {
            _navigationManager.CreateAndShowWindow<RecipeListWindow>();
            _navigationManager.CloseWindow<AddRecipeWindow>();
        }
    }
}
