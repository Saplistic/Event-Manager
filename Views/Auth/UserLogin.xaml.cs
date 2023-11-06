using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Administration;
using EventManager.Data;
using EventManager.Models;
using EventManager.Services;

namespace EventManager.Views.Auth
{
    /// <summary>
    /// Interaction logic for UserLogin.xaml
    /// </summary>
    public partial class UserLogin : Window
    {
        public UserLogin()
        {
            InitializeComponent();
        }

        private void Submit(object sender, RoutedEventArgs e)
        {
            var InputEmailAddress = EmailTB.Text;
            var InputPassword = PasswordPB.Password;

            using (var context = new MyDBContext())
            {
                Initializer.DbSetInitializer(context);
                bool userfound = context.Users.Any(user => user.EmailAddress == InputEmailAddress && user.Password == InputPassword);

                if (!userfound)
                {
                    MessageBox.Show("Invalid credentials");
                    return;
                }

                User User = context.Users
                    .Where(user => user.EmailAddress == InputEmailAddress && user.Password == InputPassword)
                    .FirstOrDefault();
                UserService.Instance.Login(User);

                if (UserService.Instance.User != null) // Als de UserService een User bevat, dan is de login geslaagd
                {
                    GrantAccess();
                }
            }
        }

        public void GrantAccess()
        {
            MainWindow mainWindow = new MainWindow();
            this.Close();
            mainWindow.Show();
        }

        private void NavigateToRegistration(object sender, RoutedEventArgs e)
        {
            UserRegistration userRegistration = new UserRegistration();
            this.Close();
            userRegistration.Show();
        }
    }
}
