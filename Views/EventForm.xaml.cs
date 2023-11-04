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
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore;
using EventManager.Models;
using Administration;
using EventManager.Data;
using EventManager.Migrations;
using Microsoft.IdentityModel.Tokens;
using ViewModels;

namespace EventManager.Views
{
    /// <summary>
    /// Interaction logic for EventForm.xaml
    /// </summary>
    public partial class EventForm : Window
    {
        MyDBContext context = new MyDBContext();
        public Event selectedEvent { get; set; }

        // Constructor gebruikt om events aan te maken (Add)
        public EventForm()
        {
            Initializer.DbSetInitializer(context);
            InitializeComponent();

            SubmitBtn.Click += CreateEvent; // Create methode aan de submit knop koppelen
        }

        // Constructor gebruikt om events te updaten (Edit)
        public EventForm(int selectedEventId)
        {
            Initializer.DbSetInitializer(context);
            InitializeComponent();

            selectedEvent = context.Events.Find(selectedEventId);

            Title.Text = "Edit an event";
            SubmitBtn.Content = "Update";

            // Vul de velden met de gegevens van de geselecteerde event
            EventNameTB.Text = selectedEvent.Name;
            EventLocationTB.Text = selectedEvent.Location;
            EventDescriptionTB.Text = selectedEvent.Description;
            EventStartDatePicker.SelectedDate = selectedEvent.StartTime;
            EventStartTimePicker.Value = selectedEvent.StartTime;
            EventEndDatePicker.SelectedDate = selectedEvent.EndTime;
            EventEndTimePicker.Value = selectedEvent.EndTime;

            DeleteBtn.Visibility = Visibility.Visible; // Delete knop zichtbaar maken

            DeleteBtn.Click += DeleteEvent; // Delete methode aan de delete knop koppelen
            SubmitBtn.Click += UpdateEvent; // Update methode aan de submit knop koppelen
        }


        private void CreateEvent(object sender, RoutedEventArgs e)
        {
            if (UserService.Instance.User == null)
            {
                MessageBox.Show("You have insufficient rights to perform this action");
                return;
            }

            if (!Validate())
            {
                return;
            }

            TimeSpan startTime = EventStartTimePicker.Value.Value.TimeOfDay;
            TimeSpan endTime = EventEndTimePicker.Value.Value.TimeOfDay;

            Event newEvent = new Event()
            {
                Name = EventNameTB.Text,
                Location = EventLocationTB.Text,
                Description = EventDescriptionTB.Text,
                StartTime = (EventStartDatePicker.SelectedDate.Value + startTime), // Tel de datum en tijd met elkaar op
                EndTime = (EventEndDatePicker.SelectedDate.Value + endTime),
                UserId = UserService.Instance.User.Id
            };

            context.Events.Add(newEvent);
            context.SaveChanges();
            MainWindow.myDataGrid.ItemsSource = context.Events.Include(e => e.User).ToList(); // Update de datagrid
            Close();
            MessageBox.Show("Event succesfully created " + newEvent.StartTime);
        }

        private void UpdateEvent(object sender, RoutedEventArgs e)
        {

            if (UserService.Instance.User == null || UserService.Instance.User.Id != selectedEvent.UserId)
            {
                MessageBox.Show("You have insufficient rights to perform this action");
                return;
            }

            if (selectedEvent == null)
            {
                MessageBox.Show("Event to update not found");
                return;
            }

            if (!Validate())
            {
                return;
            }

            TimeSpan startTime = EventStartTimePicker.Value.Value.TimeOfDay;
            TimeSpan endTime = EventEndTimePicker.Value.Value.TimeOfDay;

            selectedEvent.Name = EventNameTB.Text;
            selectedEvent.Location = EventLocationTB.Text;
            selectedEvent.Description = EventDescriptionTB.Text;
            selectedEvent.StartTime = (EventStartDatePicker.SelectedDate.Value + startTime);
            selectedEvent.EndTime = (EventEndDatePicker.SelectedDate.Value + endTime);

            context.Events.Update(selectedEvent);
            context.SaveChanges();
            MainWindow.myDataGrid.ItemsSource = context.Events.Include(e => e.User).ToList(); // Update de datagrid
            Close();
            MessageBox.Show("Event succesfully updated");
        }

        private void DeleteEvent(object sender, RoutedEventArgs e)
        {
            if (selectedEvent == null)
            {
                MessageBox.Show("Event to delete not found");
                return;
            }

            if (UserService.Instance.User == null || UserService.Instance.User.Id != selectedEvent.UserId)
            {
                MessageBox.Show("You have insufficient rights to perform this action");
                return;
            }

            context.Events.Remove(selectedEvent);
            context.SaveChanges();
            MainWindow.myDataGrid.ItemsSource = context.Events.Include(e => e.User).ToList(); // Update de datagrid
            Close();
            MessageBox.Show("Event succesfully deleted");
        }

        private bool Validate()
        {
            // Validatie voor de invoer:
            // Naam event
            if (string.IsNullOrEmpty(EventNameTB.Text))
            {
                MessageBox.Show("Please enter a name");
                return false;
            }
            else if (!(EventNameTB.Text.Count() >= 6 && EventNameTB.Text.Count() <= 40))
            {
                MessageBox.Show("Name must contain between 6 to 40 characters");
                return false;
            }
            // Locatie event
            else if (string.IsNullOrEmpty(EventLocationTB.Text))
            {
                MessageBox.Show("Please enter a location");
                return false;
            }
            else if (!(EventLocationTB.Text.Count() >= 6 && EventLocationTB.Text.Count() <= 60))
            {
                MessageBox.Show("Location must contain between 6 to 60 characters");
                return false;
            }
            // Startdatum
            else if (!EventStartDatePicker.SelectedDate.HasValue)
            {
                MessageBox.Show("Please select a date");
                return false;
            }
            // Starttijd
            else if (!EventStartTimePicker.Value.HasValue)
            {
                MessageBox.Show("Please select a starting time");
                return false;
            }
            // Einddatum
            else if (!EventEndDatePicker.SelectedDate.HasValue)
            {
                MessageBox.Show("Please select an end date");
                return false;
            }
            // Eindtijd
            else if (!EventEndTimePicker.Value.HasValue)
            {
                MessageBox.Show("Please select an end time");
                return false;
            }
            // Kijk of de starttijd voor de eindtijd is
            else if (!((EventStartDatePicker.SelectedDate.Value + EventStartTimePicker.Value.Value.TimeOfDay) < (EventEndDatePicker.SelectedDate.Value + EventEndTimePicker.Value.Value.TimeOfDay)))
            {
                MessageBox.Show("End date + time must be after start date + time");
                return false;
            }
            // Beschrijving
            else if (!(EventDescriptionTB.Text.Count() <= 600))
            {
                MessageBox.Show("Description must container less than 600 characters");
                return false;
            }
            return true;
        }

        private void Action_Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}