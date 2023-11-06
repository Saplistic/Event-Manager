using Administration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using EventManager.Models;
using EventManager.Services;

namespace EventManager.Views.Pages
{
    /// <summary>
    /// Interaction logic for EventsFeed.xaml
    /// </summary>
    public partial class EventsFeed : Page
    {
        public static ListView eventsListView = new ListView();
        private SubscriptionManager SubscriptionManager = new SubscriptionManager();
        private string SearchQuery;

        MyDBContext context = new MyDBContext();

        public EventsFeed()
        {
            InitializeComponent();

            UpdateEventsFeed();
            eventsListView = EventContainerLV;
        }

        public void UpdateEventsFeed()
        {
            var events = new List<Event>();
            if (string.IsNullOrEmpty(SearchQuery))
            {
                events = context.Events.Include(e => e.User).Include(e => e.Subscriptions).ToList();
            }
            else
            {
                events = context.Events.Include(e => e.User).Include(e => e.Subscriptions).Where(e => e.Name.Contains(SearchQuery)).ToList();
            }
            
            EventContainerLV.ItemsSource = events;
        }

        private void OpenEventAddForm(object sender, RoutedEventArgs e)
        {
            EventForm eventForm = new EventForm();
            eventForm.ShowDialog();
        }

        private void SubscribeBtnSubmit(object sender, RoutedEventArgs e)
        {
            Button senderBtn = (Button)sender;
            Event selectedEvent = (Event)senderBtn.DataContext;

            SubscriptionManager.Subscribe(selectedEvent.Id);
            UpdateEventsFeed();
        }

        private void SearchBtnSubmit(object sender, RoutedEventArgs e)
        {
            SearchQuery = SearchBarTB.Text;
            UpdateEventsFeed();
        }
    }
}
