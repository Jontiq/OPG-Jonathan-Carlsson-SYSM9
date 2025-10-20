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
        public User LoggedIn { get; private set; }

        public UserManager()
        {
            //"normal" user
            _users.Add(new User("user", "password", "sweden"));
            //"admin" user
            _users.Add(new AdminUser("admin", "password", "sweden"));
        }
    }
}
