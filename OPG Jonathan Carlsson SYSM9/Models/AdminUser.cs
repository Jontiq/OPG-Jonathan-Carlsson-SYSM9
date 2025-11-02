using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPG_Jonathan_Carlsson_SYSM9.Models
{
    //I might remove this whole class
    public class AdminUser : User
    {
        //Forced constructor because of inheritance, perhaps i should remove from userclass?
        public AdminUser(int id, string username, string password, string country, string securityQuestion, string securityAnswer, bool isAdmin) 
            : base(id, username, password, country, securityQuestion, securityAnswer, isAdmin)
        {

        }

        //The methods below should not be seen as "methods", but more as "permissions", do not forget
        public void RemoveAnyRecipe()
        {

        }

        public void ViewAllRecipes()
        {

        }
    }
}
