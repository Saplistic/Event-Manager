using Administration;
using EventManager.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.EntityFrameworkCore;

namespace EventManager.Services
{
    public class SubscriptionManager
    {
        public MyDBContext context = new MyDBContext();

        public void Subscribe(int id)
        {
            Event eventToSubscribe = context.Events.Include(e => e.Subscriptions).First(e => e.Id == id);

            if (!eventToSubscribe.Subscriptions.Any(e => e.UserId == UserService.Instance.User.Id))
            {
                Subscription subscription = new Subscription()
                {
                    UserId = UserService.Instance.User.Id,
                    EventId = eventToSubscribe.Id
                };
                context.Subscriptions.Add(subscription);
                MessageBox.Show("Subscribed to event");
            }
            else
            {
                context.Subscriptions.Remove(eventToSubscribe.Subscriptions.First(e => e.UserId == UserService.Instance.User.Id));
                MessageBox.Show("Unsubscribed to event");
            }

            context.SaveChanges();
        }

    }
}
