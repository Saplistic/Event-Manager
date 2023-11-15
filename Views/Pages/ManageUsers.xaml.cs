using Administration;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EventManager.Views.Pages
{
    /// <summary>
    /// Interaction logic for ManageUsers.xaml
    /// </summary>
    public partial class ManageUsers : Page
    {
        public static DataGrid myDataGrid = new DataGrid();
        MyDBContext context = new MyDBContext();

        public ManageUsers()
        {
            InitializeComponent();

            UpdateDataGrid();
            myDataGrid = UsersDataGrid;
        }

        public void UpdateDataGrid()
        {
            var users = context.Users.ToList();
            UsersDataGrid.ItemsSource = users;
        }

        private void OpenUserAddForm(object sender, RoutedEventArgs e)
        {
            UserForm userForm = new UserForm();
            userForm.ShowDialog();
        }

        private void OpenUserEditForm(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
