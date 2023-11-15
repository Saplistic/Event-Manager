using EventManager.Services;
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
using EventManager.Models;
using EventManager.Models.RequestModels;
using Administration;

namespace EventManager.Views
{
    /// <summary>
    /// Interaction logic for UserForm.xaml
    /// </summary>
    public partial class UserForm : Window
    {
        private UserManager userManager = new UserManager();
        private int userId;
        private User userToUpdate;

        public UserForm()
        {
            InitializeComponent();

            SubmitBtn.Click += SubmitCreateUser;
        }

        public UserForm(int userId)
        {
            this.userId = userId;

            using (var context = new MyDBContext())
            {
                this.userToUpdate = context.Users.Where(u => u.Id == userId).FirstOrDefault();
                DataContext = userToUpdate;
            }

            InitializeComponent();

            TitleTBl.Text = "Edit a user";
            SubmitBtn.Content = "Update user";

            SubmitBtn.Click += SubmitUpdateUser;
            DeleteBtn.Click += SubmitDeleteUser;

            DeleteBtn.Visibility = Visibility.Visible;
        }

        private void ExitWindow(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SubmitCreateUser(object sender, RoutedEventArgs e)
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
                MessageBox.Show("User succesfully created");
                this.Close();
            }
        }

        private void SubmitUpdateUser(object sender, RoutedEventArgs e)
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

            if (userManager.Update(userRequest, userId))
            {
                MessageBox.Show("User succesfully updated");
                this.Close();
            }
        }

        private void SubmitDeleteUser(object sender, RoutedEventArgs e)
        {
            if (userManager.Delete(userId))
            {
                MessageBox.Show("User succesfully deleted");
                this.Close();
            }
        }
    }
}
