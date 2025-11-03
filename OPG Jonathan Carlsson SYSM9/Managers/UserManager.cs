using OPG_Jonathan_Carlsson_SYSM9.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace OPG_Jonathan_Carlsson_SYSM9.Managers
{
    public class UserManager
    {
        private List<User> _users = new List<User>();
        public List<User> Users
        {
            get { return _users; }
        }
        private User LoggedIn { get; set; }

        public UserManager()
        {
            //"normal" user
            _users.Add(new User(1,"user", "password", "sweden", "write 'answer' in the answer field", "answer", false));
            //"admin" user
            _users.Add(new AdminUser(2,"admin", "password", "sweden", "write 'answer' in the answer field", "answer",true));
        }

        //Allowes user to log in if username and password match, and returns boolean value "TRUE".
        public bool LogIn(string username, string password)
        {
            foreach(User u in _users)
            {
                if(u.Username == username && u.Password == password)
                {
                    //Input 2FA here i believe
                    LoggedIn = u;
                    return true;
                }
            }
            return false;
        }
        //Allowes the user to logout
        public void LogOut()
        {
            LoggedIn = null;
        }

        //Returns which user is logged in
        public User GetLoggedIn()
        {
            return LoggedIn;
        }

        //Finds and returns a user by their ID
        public User GetUserById(int id)
        {
            foreach (User u in _users)
            {
                if (u.Id == id)
                {
                    return u;
                }
            }
            //Returns null if no user with that ID exists, but this should never happen (we'll see)
            return null;
        }

        //Changes the password for the logged in user
        public void ChangePassword(string newPassword)
        {
            LoggedIn.Password = newPassword;
        }

        //Creates and adds a new user
        public void CreateUser(string username, string password, string country, string question, string answer, bool isAdmin)
        {
            //Finds the currently highest user ID
            int highestId = 0;

            foreach (User u in _users)
            {
                if (u.Id > highestId)
                {
                    highestId = u.Id;
                }
            }
            //Sets the ID to be 1+ higher than the highest id
            int newId = highestId + 1;

            //Creates the user with the new id
            User newUser = new User(newId, username, password, country, question, answer.ToLower(), isAdmin);

            //adds it to the users
            _users.Add(newUser);
        }

        //Updates the user information
        public void UpdateUser(User updatedUser)
        {
            //Finds the user and updates the user info
            foreach (User u in Users)
            {
                if (u.Id == updatedUser.Id)
                {
                    u.Username = updatedUser.Username;
                    u.Password = updatedUser.Password;
                    u.Country = updatedUser.Country;
                    break;
                }
            }
        }
    }

}



