using Administration;
using EventManager.Data;
using EventManager.Models;
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
using EventManager.Models.RequestModels;
using EventManager.Services;

namespace EventManager.Views.Auth
{
    /// <summary>
    /// Interaction logic for UserRegistration.xaml
    /// </summary>
    public partial class UserRegistration : Window
    {
        UserManager userManager = new UserManager();

        public UserRegistration()
        {
            InitializeComponent();
        }

        private void SumbitRegistration(object sender, RoutedEventArgs e)
        {
            UserRequest userRequest = new UserRequest()
            {
                FirstName = FirstName.Text,
                LastName = LastName.Text,
                EmailAddress = Email.Text,
                Password = Password.Password,
                ConfirmPassword = ConfirmPassword.Password,
                PhoneNumber = PhoneNumber.Text
            };

            if (userManager.Create(userRequest))
            {
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
