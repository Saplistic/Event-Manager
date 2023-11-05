using Administration;
using EventManager.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using EventManager.Models;
using EventManager.Views;
using EventManager.Views.Pages;
using Microsoft.EntityFrameworkCore;
using ViewModels;

namespace EventManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public String UserName { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            // Databinding voor de naam van de ingelogde gebruiker
            UserName = UserService.Instance.User.FirstName + " " + UserService.Instance.User.LastName;
            DataContext = this;
        }

        private void NavigateToEventsFeed(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new EventsFeed());
        }

        private void NavigateToMyEvents(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new MyEvents());
        }
    }
}
