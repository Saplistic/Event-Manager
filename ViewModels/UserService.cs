using EventManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class UserService
    {
        private static UserService _instance;
        private User user;

        private UserService() { }

        public static UserService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UserService();
                }
                return _instance;
            }
        }

        public User User
        {
            get { return user; }
            set { user = value; }
        }

        public void Login(User user)
        {
            this.user = user;
        }

        public void Logout()
        {
            this.user = null;
        }
    }
}
