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
        public string Title { get; set; }
        public List<string> Ingredients { get; set; }
        public string Instructions { get; set; }
        public string Category { get; set; }
        public DateTime Date { get; set; }
        public User CreatedBy { get; set; }

        //constructors
        //for testing if needed
        public Recipe() { }

        public Recipe(string title, List<string> ingredients, string instructions, string category, DateTime date, User createdBy)
        {
            Title = title;
            Ingredients = ingredients;
            Instructions = instructions;
            Category = category;
            Date = date;
            CreatedBy = createdBy;
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
