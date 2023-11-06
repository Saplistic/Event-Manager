using Administration;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore;
using EventManager.Services;

namespace EventManager.Views.Pages
{
    /// <summary>
    /// Interaction logic for SubscribedEvents.xaml
    /// </summary>
    public partial class SubscribedEvents : Page
    {
        public static ListView eventsListView = new ListView();
        private SubscriptionManager SubscriptionManager = new SubscriptionManager();
        MyDBContext context = new MyDBContext();


        public SubscribedEvents()
        {
            InitializeComponent();

            UpdateEventsFeed();
            eventsListView = EventContainerLV;
        }

        public void UpdateEventsFeed()
        {
            // Laad alle Abonnementen van de ingelogde gebruiker met bijbehorende events
            var events = context.Subscriptions.Where(e => e.UserId == UserService.Instance.User.Id)
                                                                .Include(e => e.Event).ToList();
            EventContainerLV.ItemsSource = events;
        }

        private void SubscribeBtnSubmit(object sender, RoutedEventArgs e)
        {
            Button senderBtn = (Button)sender;
            Subscription selectedSubscription = (Subscription)senderBtn.DataContext;

            SubscriptionManager.Subscribe(selectedSubscription.EventId);
            UpdateEventsFeed();
        }

    }
}
