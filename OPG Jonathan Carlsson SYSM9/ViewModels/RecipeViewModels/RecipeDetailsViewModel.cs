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
    public class RecipeDetailsViewModel : BaseViewModel
    {
        //props
        private readonly RecipeManager _recipeManager;
        private readonly UserManager _userManager;
        private readonly NavigationManager _navigationManager;

        //Will store the selected recipe from RecipeListWindow, which lets us set the rest of the "normal" prop values
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
        private string _category;
        public string Category
        {
            get { return _category; }
            set 
            {
                _category = value;
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

        private string _createdByUsername;
        public string CreatedByUsername
        {
            get { return _createdByUsername; }
            set 
            { 
                _createdByUsername = value;
                OnPropertyChanged();
            }
        }

        private DateTime _creationDate;
        public DateTime CreationDate
        {
            get { return _creationDate; }
            set 
            {
                _creationDate = value;
                OnPropertyChanged();
            }
        }
        //If true, the props are in editing-mode
        private bool _isReadOnly;
        public bool IsReadOnly
        {
            get { return _isReadOnly; }
            set
            {
                _isReadOnly = value;
                OnPropertyChanged();
            }
        }


        //Lets the user cancel registrating new recipe and goes back to RecipeListWindow
        public ICommand GoBackCommand { get; }
        //Activates the edit-possibilities
        public ICommand EditCommand { get; }

        //constructor
        public RecipeDetailsViewModel()
        {
            _recipeManager = (RecipeManager)Application.Current.Resources["RecipeManager"];
            _userManager = (UserManager)Application.Current.Resources["UserManager"];
            _navigationManager = (NavigationManager)Application.Current.Resources["NavigationManager"];

            IsReadOnly = true;

            GoBackCommand = new RelayCommand(execute => ExecuteGoBack());
            EditCommand = new RelayCommand(execute => ExecuteEdit());


            //Assigns the values from the SelectedRecipe in _recipeManager
            Title = _recipeManager.SelectedRecipe.Title;
            Category = _recipeManager.SelectedRecipe.Category;
            Instructions = _recipeManager.SelectedRecipe.Instructions;
            Ingredients = _recipeManager.SelectedRecipe.Ingredients;
            CreatedByUsername = _userManager.GetUserById(_recipeManager.SelectedRecipe.CreatedByID).Username;
            CreationDate = _recipeManager.SelectedRecipe.Date;

        }

        //methods

        private void ExecuteGoBack()
        {

            if (!IsReadOnly)
            {
                MessageBoxResult result = MessageBox.Show(
                    "Would you like to save your changes?",
                    "Confirm",
                    MessageBoxButton.YesNoCancel,
                    MessageBoxImage.Question
                );
                //The user is not sent back
                if (result == MessageBoxResult.Cancel)
                {
                    return;
                }
                if (result == MessageBoxResult.Yes)
                {
                    //HERE THE CHANGES WILL BE SAVED, I JUST NEED TO WRITE THE METHOD FOR SAVING
                    _recipeManager.SelectedRecipe = null;
                    _navigationManager.CreateAndShowWindow<RecipeListWindow>();
                    _navigationManager.CloseWindow<RecipeDetailsWindow>();
                    return;
                }
            }
            _recipeManager.SelectedRecipe = null;
            _navigationManager.CreateAndShowWindow<RecipeListWindow>();
            _navigationManager.CloseWindow<RecipeDetailsWindow>();
        }

        public void ExecuteEdit()
        {
            MessageBoxResult result = MessageBox.Show(
                "Would you like to edit this recipe?",
                "Confirm",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );

            if (result == MessageBoxResult.Yes)
            {
                IsReadOnly = false;
            }
        }


    }
}
