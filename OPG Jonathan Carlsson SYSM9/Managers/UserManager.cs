using OPG_Jonathan_Carlsson_SYSM9.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
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
            _users.Add(new User("user", "password", "sweden", "write 'answer' in the answer field", "answer", false));
            //"admin" user
            _users.Add(new AdminUser("admin", "password", "sweden", "write 'answer' in the answer field", "answer",true));
        }

        //Allowes user to log in if username and password match, and returns boolean value "TRUE".
        public bool LogIn(string username, string password)
        {
            foreach(User u in _users)
            {
                if(u.Username == username && u.Password == password)
                {
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

        //Changes the password for the logged in user
        public void ChangePassword(string newPassword)
        {
            LoggedIn.Password = newPassword;
        }
    }
}
