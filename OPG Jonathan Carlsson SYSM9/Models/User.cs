using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPG_Jonathan_Carlsson_SYSM9.Models
{
    public class User
    {
        //props
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Country { get; set; }
        public string SecurityQuestion { get; set; }
        public string SecurityAnswer { get; set; }
        public bool IsAdmin { get; set; }

        //Should i add a boolean for Admin perhaps? whereas Admin == true / false? This may make it easier later?

        //constructor
        public User(int id, string username, string password, string country, string securityQuestion, string securityAnswer, bool isAdmin)
        {
            Id = id;
            Username = username;
            Password = password;
            Country = country;
            SecurityQuestion = securityQuestion;
            SecurityAnswer = securityAnswer;
            IsAdmin = isAdmin;
        }

        //methods
        
    }
}
