using EventManager.Models;
using EventManager.Views.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using EventManager.Models.RequestModels;
using Microsoft.EntityFrameworkCore;
using Administration;

namespace EventManager.Services
{
    public class EventManager
    {

        public bool Create(EventRequest eventRequest)
        {
            if (UserService.Instance.User == null)
            {
                MessageBox.Show("You have insufficient rights to perform this action");
                return false;
            }

            if (!Validate(eventRequest))
            {
                return false;
            }

            Event newEvent = new Event()
            {
                Name = eventRequest.Name,
                Location = eventRequest.Location,
                Description = eventRequest.Description,
                StartTime = (eventRequest.StartDate.Value + eventRequest.StartTime.Value), // Tel de datum en tijd van de dag met elkaar op
                EndTime = (eventRequest.EndDate.Value + eventRequest.EndTime.Value),
                UserId = UserService.Instance.User.Id
            };

            using (var context = new MyDBContext())
            {
                context.Add(newEvent);
                context.SaveChanges();
            }
            return true;
        }

        public bool Update(EventRequest eventRequest, int eventId)
        {
            using (var context = new MyDBContext())
            {

                Event selectedEvent = context.Events.Find(eventId);

                if (selectedEvent == null)
                {
                    MessageBox.Show("Event to update not found");
                    return false;
                }

                if (UserService.Instance.User == null || selectedEvent.UserId != UserService.Instance.User.Id)
                {
                    MessageBox.Show("You have insufficient rights to perform this action");
                    return false;
                }

                if (!Validate(eventRequest))
                {
                    return false;
                }

                {
                    selectedEvent.Name = eventRequest.Name;
                    selectedEvent.Location = eventRequest.Location;
                    selectedEvent.Description = eventRequest.Description;
                    selectedEvent.StartTime = (eventRequest.StartDate.Value + eventRequest.StartTime.Value); // Tel de datum en tijd van de dag met elkaar op
                    selectedEvent.EndTime = (eventRequest.EndDate.Value + eventRequest.EndTime.Value);
                }

                context.Events.Update(selectedEvent);
                context.SaveChanges();
            }
            return true;
        }

        public bool Delete(int eventId)
        {

            using (var context = new MyDBContext())
            {

                Event selectedEvent = context.Events.Find(eventId);

                if (selectedEvent == null)
                {
                    MessageBox.Show("Event to delete not found");
                    return false;
                }

                if (UserService.Instance.User == null || selectedEvent.UserId != UserService.Instance.User.Id)
                {
                    MessageBox.Show("You have insufficient rights to perform this action");
                    return false;
                }

                context.Subscriptions.RemoveRange(context.Subscriptions.Where(s => s.EventId == selectedEvent.Id));
                context.Events.Remove(selectedEvent);
                context.SaveChanges();
            }
            return true;
        }

        private bool Validate(EventRequest eventRequest)
        {
            // Validatie voor de invoer:
            // Naam event
            if (string.IsNullOrEmpty(eventRequest.Name))
            {
                MessageBox.Show("Please enter a name");
                return false;
            }
            else if (!(eventRequest.Name.Count() >= 6 && eventRequest.Name.Count() <= 40))
            {
                MessageBox.Show("Name must contain between 6 to 40 characters");
                return false;
            }
            // Locatie event
            else if (string.IsNullOrEmpty(eventRequest.Location))
            {
                MessageBox.Show("Please enter a location");
                return false;
            }
            else if (!(eventRequest.Location.Count() >= 6 && eventRequest.Location.Count() <= 60))
            {
                MessageBox.Show("Location must contain between 6 to 60 characters");
                return false;
            }
            // Startdatum
            else if (!eventRequest.StartDate.HasValue)
            {
                MessageBox.Show("Please select a date");
                return false;
            }
            // Starttijd
            else if (!eventRequest.StartTime.HasValue)
            {
                MessageBox.Show("Please select a starting time");
                return false;
            }
            // Einddatum
            else if (!eventRequest.EndDate.HasValue)
            {
                MessageBox.Show("Please select an end date");
                return false;
            }
            // Eindtijd
            else if (!eventRequest.EndTime.HasValue)
            {
                MessageBox.Show("Please select an end time");
                return false;
            }
            // Kijk of de starttijd voor de eindtijd is
            else if (!((eventRequest.StartDate.Value + eventRequest.StartTime.Value) < (eventRequest.EndDate.Value + eventRequest.EndTime.Value)))
            {
                MessageBox.Show("End date + time must be after start date + time");
                return false;
            }
            // Beschrijving
            else if (!(eventRequest.Description.Count() <= 600))
            {
                MessageBox.Show("Description must container less than 600 characters");
                return false;
            }
            return true;
        }

    }
}
