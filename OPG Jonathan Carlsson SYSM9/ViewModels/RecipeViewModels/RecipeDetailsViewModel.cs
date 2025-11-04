using MVVM_KlonaMIg.MVVM;
using OPG_Jonathan_Carlsson_SYSM9.Managers;
using OPG_Jonathan_Carlsson_SYSM9.Models;
using OPG_Jonathan_Carlsson_SYSM9.Views;
using OPG_Jonathan_Carlsson_SYSM9.Views.RecipeWindows;
using System;
using System.CodeDom;
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
        //Allowes the user to save the recipe
        public ICommand SaveCommand { get; }
        //Allowes the user to copy the recipe
        public ICommand CopyCommand { get; }

        //constructor
        public RecipeDetailsViewModel()
        {
            _recipeManager = (RecipeManager)Application.Current.Resources["RecipeManager"];
            _userManager = (UserManager)Application.Current.Resources["UserManager"];
            _navigationManager = (NavigationManager)Application.Current.Resources["NavigationManager"];

            IsReadOnly = true;

            GoBackCommand = new RelayCommand(execute => ExecuteGoBack());
            EditCommand = new RelayCommand(execute => ExecuteEdit(), canExecute => CanExecuteEdit());
            SaveCommand = new RelayCommand(execute => ExecuteSaveRecipeChanges(),canExecute => CanExecuteSaveRecipeChanges());
            CopyCommand = new RelayCommand(execute => ExecuteCopyRecipe(), canExecute => CanExecuteEdit());


            //Assigns the values from the SelectedRecipe in _recipeManager
            Title = _recipeManager.SelectedRecipe.Title;
            Category = _recipeManager.SelectedRecipe.Category;
            Instructions = _recipeManager.SelectedRecipe.Instructions;
            Ingredients = _recipeManager.SelectedRecipe.Ingredients;
            CreatedByUsername = _userManager.GetUserById(_recipeManager.SelectedRecipe.CreatedByID).Username;
            CreationDate = _recipeManager.SelectedRecipe.Date;

        }

        //methods

        //Lets the user return to the recipe list window
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
                    ExecuteSaveRecipeChanges();
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
        //Activated editable fields
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

        public bool CanExecuteEdit()
        {
            if (!IsReadOnly)
            {
                return false;
            }

            return true;
        }
        //Saves the recipe in it's current condition
        private void ExecuteSaveRecipeChanges()
        {
            //Create a new recipe with the same ID as the selectedrecipe ID
            Recipe updatedRecipe = new Recipe(
                _recipeManager.SelectedRecipe.Id,
                Title,
                Ingredients,
                Instructions,
                Category,
                //Keps the old date and the createdByID.
                _recipeManager.SelectedRecipe.Date,
                _recipeManager.SelectedRecipe.CreatedByID
            );

            //Calls the "UpdateRecipe" method from RecipeManager and updates the recipe
            _recipeManager.UpdateRecipe(updatedRecipe);

            //Now the selectedRecipe is the updated one (just in case :) )
            _recipeManager.SelectedRecipe = updatedRecipe;

            //Locks field, so that the user needs to click "edit" to onlock again
            IsReadOnly = true;
            //feedback :)
            MessageBox.Show("Changes saved successfully!", "Saved", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private bool CanExecuteSaveRecipeChanges()
        {
            //Checks so that no fields is empty while editing
            if (string.IsNullOrWhiteSpace(Title))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(Ingredients))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(Instructions))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(Category))
            {
                return false;
            }

            //All fields have data
            return true;
        }

        private void ExecuteCopyRecipe()
        {
            //Creates a new AddRecipeWindow
            AddRecipeWindow copyWindow = new AddRecipeWindow();

            //Creates a new ViewModel, with the exact same props as the selected item
            AddRecipeViewModel copyViewModel = new AddRecipeViewModel(
                Title,
                Ingredients,
                Instructions,
                Category
            );

            //"Connects" them, as we do in code-behind usually
            copyWindow.DataContext = copyViewModel;

            //Shows the copied window
            copyWindow.Show();
            _navigationManager.CloseWindow<RecipeDetailsWindow>();
        }


    }
}
