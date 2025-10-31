using OPG_Jonathan_Carlsson_SYSM9.Managers;
using OPG_Jonathan_Carlsson_SYSM9.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OPG_Jonathan_Carlsson_SYSM9.ViewModels.RecipeViewModels
{
    public class RecipeDetailsViewModel : BaseViewModel
    {
        //props
        private readonly RecipeManager _recipeManager;
        private readonly UserManager _userManager;
        private readonly NavigationManager _navigationManager;

        //Will store the selected recipe from RecipeListWindow
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

        //constructor
        public RecipeDetailsViewModel()
        {
            _recipeManager = (RecipeManager)Application.Current.Resources["RecipeManager"];
            _userManager = (UserManager)Application.Current.Resources["UserManager"];
            _navigationManager = (NavigationManager)Application.Current.Resources["NavigationManager"];

            Title = _recipeManager.SelectedRecipe.Title;
            Category = _recipeManager.SelectedRecipe.Category;
            Instructions = _recipeManager.SelectedRecipe.Instructions;
            Ingredients = _recipeManager.SelectedRecipe.Ingredients;
            CreatedByUsername = _userManager.GetUserById(_recipeManager.SelectedRecipe.CreatedByID).Username;
            CreationDate = _recipeManager.SelectedRecipe.Date;

        }

        //methods
    }
}
