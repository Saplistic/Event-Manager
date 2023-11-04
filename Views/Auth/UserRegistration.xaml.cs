using Administration;
using EventManager.Data;
using EventManager.Model;
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

namespace EventManager.Views.Auth
{
    /// <summary>
    /// Interaction logic for UserRegistration.xaml
    /// </summary>
    public partial class UserRegistration : Window
    {
        public UserRegistration()
        {
            InitializeComponent();
        }

        private void SumbitRegistration(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(FirstName.Text))
            {
                MessageBox.Show("Please enter your first name");
                return;
            }
            else if (FirstName.Text.Count() < 2 && FirstName.Text.Count() <= 40)
            {
                MessageBox.Show("First name must contain between 2 to 60 characters");
                return;
            }

            else if (string.IsNullOrEmpty(LastName.Text))
            {
                MessageBox.Show("Please enter your last name");
                return;
            }
            else if (LastName.Text.Count() < 2 && FirstName.Text.Count() > 40)
            {
                MessageBox.Show("Last name may only contain between 2 to 60 characters");
                return;
            }

            else if (string.IsNullOrEmpty(Email.Text))
            {
                MessageBox.Show("Please enter your email address");
                return;
            }

            else if (Email.Text.Count() < 6 && Email.Text.Count() > 50)
            {
                MessageBox.Show("Email address may only contain between 6 to 50 characters");
                return;
            }

            else if (string.IsNullOrEmpty(Password.Password))
            {
                MessageBox.Show("Please enter your password");
                return;
            }
            else if (Password.Password.Count() < 5 && Password.Password.Count() > 40)
            {
                MessageBox.Show("Password may only contain between 5 to 40 characters");
                return;
            }

            else if (string.IsNullOrEmpty(ConfirmPassword.Password))
            {
                MessageBox.Show("Please confirm your password");
                return;
            }
            else if (!string.Equals(Password.Password, ConfirmPassword.Password, StringComparison.Ordinal))
            {
                MessageBox.Show("Passwords do not match");
                return;
            }

            else if (!string.IsNullOrEmpty(PhoneNumber.Text) && PhoneNumber.Text.Count() > 16)
            {
                return;
            }

            using (var context = new MyDBContext())
            {
                Initializer.DbSetInitializer(context);
                
                if (context.Users.Any(user => user.EmailAddress == Email.Text))
                {
                    MessageBox.Show("Email address already in use");
                    return;
                }

                User user = new User()
                {
                    FirstName = FirstName.Text,
                    LastName = LastName.Text,
                    EmailAddress = Email.Text,
                    Password = Password.Password,
                    PhoneNumber = PhoneNumber.Text
                };

                context.Users.Add(user);
                context.SaveChanges();
                MessageBox.Show("Successfully registrated an account");
                NavigateToLogin(sender, e);
            }
            
        }

        public void NavigateToLogin(object sender, RoutedEventArgs e)
        {
            UserLogin userLogin = new UserLogin();
            userLogin.Show();
            this.Close();
        }
    }
}
