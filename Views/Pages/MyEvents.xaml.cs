using Administration;
using EventManager.Data;
using EventManager.Models;
using Microsoft.EntityFrameworkCore;
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
using EventManager.Services;

namespace EventManager.Views.Pages
{
    /// <summary>
    /// Interaction logic for Events.xaml
    /// </summary>
    public partial class MyEvents : Page
    {
        public static DataGrid myDataGrid = new DataGrid();
        MyDBContext context = new MyDBContext();

        public MyEvents()
        {
            InitializeComponent();

            UpdateDataGrid();
            myDataGrid = EventsDataGrid;
        }

        public void UpdateDataGrid()
        {
            var userEvents = context.Events.Where(e => e.UserId == UserService.Instance.User.Id).ToList();
            //var events = context.Events.Include(e => e.User).ToList();
            EventsDataGrid.ItemsSource = userEvents;
        }

        private void OpenEventAddForm(object sender, RoutedEventArgs e)
        {
            EventForm eventForm = new EventForm();
            eventForm.ShowDialog();
        }

        private void OpenEventEditForm(object sender, RoutedEventArgs e)
        {
            Event selectedEvent = (Event)EventsDataGrid.SelectedItem;
            EventForm eventForm = new EventForm(selectedEvent.Id);
            eventForm.ShowDialog();
        }
    }
}
