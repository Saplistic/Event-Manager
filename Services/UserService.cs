using System;
using System.Linq;
using System.Windows;
using Administration;
using EventManager.Models;

namespace EventManager.Services
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

        public void Login(User requestedUser)
        {
            //Probeer de meegegeven User in de database te vinden, zodat gecontroleerd kan worden of de User wel degelijk bestaat
            try
            {
                using (var context = new MyDBContext())
                {
                    context.Users.Where(u => u.EmailAddress == requestedUser.EmailAddress && u.Password == requestedUser.Password).First();
                }
            }
            catch
            {
                MessageBox.Show("User not found");
                return;
            }

            this.user = requestedUser;
            MessageBox.Show($"Succesfully logged in as {Instance.User.FirstName} {Instance.User.LastName}");
        }

        public void Logout()
        {
            this.user = null;
        }
    }
}
