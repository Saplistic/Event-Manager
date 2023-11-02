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
using EventManager.Model;
using Administration;
using EventManager.Data;
using EventManager.Migrations;

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
            // Validatie van de datums & tijden
            if (!EventStartDatePicker.SelectedDate.HasValue) //TODO: validatie voor alle velden
            {
                MessageBox.Show("Please select a date");
                return;
            } else if (!EventStartTimePicker.Value.HasValue)
            {
                MessageBox.Show("Please select a starting time");
                return;
            } else if (!EventEndDatePicker.SelectedDate.HasValue)
            {
                MessageBox.Show("Please select an end date");
                return;
            } else if (!EventEndTimePicker.Value.HasValue)
            {
                MessageBox.Show("Please select an end time");
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
                EndTime = (EventEndDatePicker.SelectedDate.Value + endTime)
            };

            context.Events.Add(newEvent);
            context.SaveChanges();
            MainWindow.myDataGrid.ItemsSource = context.Events.ToList(); // Refresh de datagrid
            Close();
            MessageBox.Show("Event succesfully created " + newEvent.StartTime);
        }

        private void UpdateEvent(object sender, RoutedEventArgs e)
        {
            if (selectedEvent == null)
            {
                MessageBox.Show("Event to update not found");
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
            MainWindow.myDataGrid.ItemsSource = context.Events.ToList(); // Refresh de datagrid
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

            context.Events.Remove(selectedEvent);
            context.SaveChanges();
            MainWindow.myDataGrid.ItemsSource = context.Events.ToList(); // Refresh de datagrid
            Close();
            MessageBox.Show("Event succesfully deleted");
        }

        private void Action_Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}