using OPG_Jonathan_Carlsson_SYSM9.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public List<Recipe> GetByUser(User user)
        {
            //Creates a new list for all the recipes by the user
            List<Recipe> userRecipes = new List<Recipe>();

            //Searches throught all of the recipes and adds the recipes created by the user to userRecipes
            foreach (Recipe r in Recipes)
            {
                if (r.CreatedBy == user)
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
