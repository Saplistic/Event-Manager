using Administration;
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
using Microsoft.EntityFrameworkCore;
using EventManager.Data;

namespace EventManager.Views.Pages
{
    /// <summary>
    /// Interaction logic for EventsFeed.xaml
    /// </summary>
    public partial class EventsFeed : Page
    {
        MyDBContext context = new MyDBContext();
        public EventsFeed()
        {
            InitializeComponent();
            Initializer.DbSetInitializer(context);

            UpdateEventsFeed();
        }

        public void UpdateEventsFeed()
        {
            var events = context.Events.Include(e => e.User).ToList();
            EventContainerLV.ItemsSource = events;
        }

        private void OpenEventAddForm(object sender, RoutedEventArgs e)
        {
            EventForm eventForm = new EventForm();
            eventForm.ShowDialog();
        }
    }
}
