using OPG_Jonathan_Carlsson_SYSM9.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OPG_Jonathan_Carlsson_SYSM9.Managers
{
    public class RecipeManager
    {
        //props
        private List<Recipe> _recipes = new List<Recipe>();
        public List<Recipe> Recipes
        {
            get { return _recipes; }
        }

        //constructor (here i'll add the recipes supposed to be created when launching the program later
        public RecipeManager()
        {
            //Recipes registered to userId 1, AKA the "normal" user.
            AddRecipe(new Recipe(
                0,
                "Spaghetti Bolognese",
                new List<string> { "Spaghetti", "Minced meat", "Tomato sauce", "Onion", "Garlic" },
                "1. Boil spaghetti\n2. Fry the minced meat\n3. Mix EVERYTHING else and serve :)",
                "Dinner",
                new DateTime(2025, 10, 30),
                1));
            //Recipes registered to userId 2, AKA the "admin" user.
            AddRecipe(new Recipe(
                0,
                "Pancakes",
                new List<string> { "Milk", "Eggs", "Flour" },
                "Mix and fry.",
                "Breakfast",
                DateTime.Now,
                2));
        }

        //methods
        //Adds recipe to Recipes list
        public void AddRecipe(Recipe recipe)
        {
            int highestId = 1;

            //Finds the max id and sets this to nextId+1
            foreach (Recipe r in Recipes)
            {
                if (r.Id > highestId)
                {
                    highestId = r.Id;
                }
            }
            recipe.Id = highestId + 1;
            Recipes.Add(recipe);
        }

        //Removes recipe from Recipes list
        public void RemoveRecipe(Recipe r)
        {
            Recipes.Remove(r);
        }
        //Returns the recipes created by everyone, to be used by Admin
        public List<Recipe> GetAllRecipes()
        {
            return Recipes;
        }
        //Returns the recipes created by the logged in user, to be used by "normal" users.

        // Returns the recipes created by a specific user ID
        public List<Recipe> GetByUser(int userID)
        {
            List<Recipe> userRecipes = new List<Recipe>();

            foreach (Recipe r in Recipes)
            {
                if (r.CreatedByID == userID)
                {
                    userRecipes.Add(r);
                }
            }

            return userRecipes;
        }

        //I will add a filter method here---------------------------------------------------------------------------

        //Looks through all the recipes til it finds a matching ID, and replaces that recipe with the new one.
        public void UpdateRecipe(Recipe updatedRecipe)
        {
            for (int i = 0; i < Recipes.Count; i++)
            {
                if (Recipes[i].Id == updatedRecipe.Id)
                {
                    Recipes[i] = updatedRecipe;
                    break;
                }
            }
        }

    }
}
