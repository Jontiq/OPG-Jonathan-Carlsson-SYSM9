using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPG_Jonathan_Carlsson_SYSM9.Models
{
    public class Recipe
    {
        //props
        //Unique ID for each recipe
        public int Id { get; set; }
        public string Title { get; set; }
        public List<string> Ingredients { get; set; }
        public string Instructions { get; set; }
        public string Category { get; set; }
        public DateTime Date { get; set; }
        public int CreatedByID { get; set; }

        //constructors

        public Recipe(int id, string title, List<string> ingredients, string instructions, string category, DateTime date, int createdByID)
        {
            Id = id;
            Title = title;
            Ingredients = ingredients;
            Instructions = instructions;
            Category = category;
            Date = date;
            CreatedByID = createdByID;
        }

        //methods (commands i guess later)
        public void EditRecipe()
        {

        }
        public void CopyRecipe()
        {

        }
    }
}
