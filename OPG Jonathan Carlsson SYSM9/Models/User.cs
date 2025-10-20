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
        public string Username { get; set; }
        public string Password { get; set; }
        public string Country { get; set; }

        //Metoden kanske borde ligga i viewmodel istället? flyttar sen när jag skapat viewmodeln
        //public string Password
        //{
        //    get { return _password; }
        //    set
        //    {
        //        if (string.IsNullOrWhiteSpace(value))
        //        {
        //            throw new ArgumentException("Password cannot be empty.");
        //        }
        //        if(value.Length < 8)
        //        {
        //            throw new ArgumentException("Password must be at least eight characters long.");
        //        }

        //        //Checks if password contains a digit
        //        bool hasDigit = false;
        //        foreach(char c in value)
        //        {
        //            if (char.IsDigit(c))
        //            {
        //                hasDigit = true;
        //                break;
        //            }
        //        }
        //        if (!hasDigit)
        //        {
        //            throw new ArgumentException("Password must contain at least one number.");
        //        }

        //        //Checks if password contains a special character (AKA if it's not letter or digit, meaning it would be a special character)
        //        bool hasSpecial = false;
        //        foreach(char c in value)
        //        {
        //            if (!char.IsLetterOrDigit(c))
        //            {
        //                hasSpecial = true;
        //                break;
        //            }
        //        }
        //        if (!hasSpecial)
        //        {
        //            throw new ArgumentException("Password must contain at least one special character");
        //        }
        //        _password = value;
        //    }
        //}


        //constructor

        public User(string username, string password, string country)
        {
            Username = username;
            Password = password;
            Country = country;
        }

        //methods
        
    }
}
