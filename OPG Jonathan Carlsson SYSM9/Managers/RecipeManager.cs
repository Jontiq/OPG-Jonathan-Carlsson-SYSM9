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
        public Recipe SelectedRecipe { get; set; }

        //constructor (here i'll add the recipes supposed to be created when launching the program later
        public RecipeManager()
        {
            //Recipes registered to userId 1, AKA the "normal" user.
            AddRecipe(new Recipe(
                0,
                "Spaghetti Bolognese",
                "Spaghetti\nMinced meat\nTomato sauce\nOnion\nGarlic",
                "1. Boil spaghetti\n2. Fry the minced meat\n3. Mix EVERYTHING else and serve :)",
                "Dinner",
                new DateTime(2025, 10, 30),
                1));
            AddRecipe(new Recipe(
                0,
                "Creamy Chicken Alfredo",
                "Fettuccine\nChicken breast\nCream\nParmesan cheese\nGarlic\nButter",
                "1. Cook the pasta\n2. Fry the chicken until golden\n3. Add butter, garlic, cream, and cheese to make the sauce\n4. Mix it all together and enjoy!",
                "Dinner",
                new DateTime(2025, 10, 30),
                1));
            //Recipes registered to userId 2, AKA the "admin" user.
            AddRecipe(new Recipe(
                0,
                "Pancakes",
                "Milk\nEggs\nFlour",
                "Mix and fry.",
                "Breakfast",
                new DateTime(2025, 10, 30),
                2));
            AddRecipe(new Recipe(
                0,
                "Taco Bowl",
                "Rice\nMinced beef\nCorn\nBeans\nLettuce\nTomato\nSour cream\nCheese",
                "1. Cook the rice\n2. Fry the minced beef with taco seasoning\n3. Add toppings and build your own bowl!",
                "Dinner",
                new DateTime(2025, 11, 4),
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
            //If the date is "default" the current date is taken. Otherwise it is the input. 
            //I've done this only because i have added a certain date to the "pre-made" recipes.
            if (recipe.Date == default(DateTime))
            {
                recipe.Date = DateTime.Now;
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
