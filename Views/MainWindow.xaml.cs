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
using Microsoft.EntityFrameworkCore;

namespace EventManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public static DataGrid myDataGrid = new DataGrid();
        MyDBContext context = new MyDBContext();
        //public ObservableCollection<Event> Events { get; set; }
        
        public MainWindow()
        {
            Initializer.DbSetInitializer(context);

            InitializeComponent();

            UpdateDataGrid();
            myDataGrid = EventsDataGrid;

        }

        public void UpdateDataGrid()
        {
            var events = context.Events.Include(e => e.User).ToList();
            EventsDataGrid.ItemsSource = events;
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
