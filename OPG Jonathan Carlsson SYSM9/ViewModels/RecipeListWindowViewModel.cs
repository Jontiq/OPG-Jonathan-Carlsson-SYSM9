using OPG_Jonathan_Carlsson_SYSM9.Managers;
using OPG_Jonathan_Carlsson_SYSM9.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OPG_Jonathan_Carlsson_SYSM9.ViewModels
{
    public class RecipeListWindowViewModel : BaseViewModel
    {
        //props
        private readonly RecipeManager _recipeManager;
        private readonly UserManager _userManager;

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

        //Will store the observable recipes for the specific logged in user OR all if they have an Admin role.
        public ObservableCollection<Recipe> Recipes { get; set; }

        //Constructor

        public RecipeListWindowViewModel()
        {
            _recipeManager = (RecipeManager)Application.Current.Resources["RecipeManager"];
            _userManager = (UserManager)Application.Current.Resources["UserManager"];

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
    }
}
