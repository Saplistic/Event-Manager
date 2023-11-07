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
using EventManager.Services;
using Administration;
using EventManager.Data;
using EventManager.Migrations;
using EventManager.Models.RequestModels;
using EventManager.Views.Pages;
using Microsoft.IdentityModel.Tokens;
using EventManager.Services;
using Xceed.Wpf.Toolkit;
using MessageBox = System.Windows.MessageBox;

namespace EventManager.Views
{
    /// <summary>
    /// Interaction logic for EventForm.xaml
    /// </summary>
    public partial class EventForm : Window
    {
        private Services.EventManager EventManager = new();
        private int eventId;
        private Event eventToUpdate;

        public EventForm()
        {
            InitializeComponent();

            SubmitBtn.Click += SubmitAddEvent; // Create methode aan de submit knop koppelen
        }

        public EventForm(int eventId)
        {
            this.eventId = eventId;

            using (var context = new MyDBContext())
            {
                this.eventToUpdate = context.Events.Where(e => e.Id == eventId).FirstOrDefault();
                DataContext = eventToUpdate;
            }
            InitializeComponent();

            Title.Text = "Edit an event";
            SubmitBtn.Content = "Update";

            DeleteBtn.Visibility = Visibility.Visible; // Delete knop zichtbaar maken

            // Vul de velden met datum  
            EventStartDatePicker.SelectedDate = eventToUpdate.StartTime;
            EventStartTimePicker.Value = eventToUpdate.StartTime;
            EventEndDatePicker.SelectedDate = eventToUpdate.EndTime;
            EventEndTimePicker.Value = eventToUpdate.EndTime;

            // Delete & Update methode aan de knoppen koppelen
            DeleteBtn.Click += SubmitDeleteEvent;
            SubmitBtn.Click += SumbitUpdateEvent;
        }

        private void Action_Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SubmitAddEvent(object sender, RoutedEventArgs e)
        {
            EventRequest eventRequest = new EventRequest()
            {
                Name = EventNameTB.Text,
                Location = EventLocationTB.Text,
                Description = EventDescriptionTB.Text,
                StartDate = EventStartDatePicker.SelectedDate,
                StartTime = EventStartTimePicker.Value.HasValue ? (TimeSpan?)EventStartTimePicker.Value.Value.TimeOfDay : null, // Converteer een nullable datetime naar een nullable timespan
                EndDate = EventEndDatePicker.SelectedDate,
                EndTime = EventEndTimePicker.Value.HasValue ? (TimeSpan?)EventEndTimePicker.Value.Value.TimeOfDay : null
            };

            if (EventManager.Create(eventRequest))
            {
                Close();
                using (var context = new MyDBContext())
                {
                    MyEvents.myDataGrid.ItemsSource = context.Events.Where(e => e.UserId == UserService.Instance.User.Id).ToList(); // Update de datagrid
                }
                MessageBox.Show("Event succesfully created");
            }
        }

        private void SumbitUpdateEvent(object sender, RoutedEventArgs e)
        {
            EventRequest eventRequest = new EventRequest()
            {
                Name = eventToUpdate.Name,
                Location = eventToUpdate.Location,
                Description = eventToUpdate.Description,
                StartDate = EventStartDatePicker.SelectedDate,
                StartTime = EventStartTimePicker.Value.HasValue ? (TimeSpan?)EventStartTimePicker.Value.Value.TimeOfDay : null, // Converteer een nullable datetime naar een nullable timespan
                EndDate = EventEndDatePicker.SelectedDate,
                EndTime = EventEndTimePicker.Value.HasValue ? (TimeSpan?)EventEndTimePicker.Value.Value.TimeOfDay : null
            };

            if (EventManager.Update(eventRequest, eventId))
            {
                Close();
                using (var context = new MyDBContext())
                {
                    MyEvents.myDataGrid.ItemsSource = context.Events.Where(e => e.UserId == UserService.Instance.User.Id).ToList(); // Update de datagrid
                }
                MessageBox.Show("Event succesfully updated");
            }
        }

        private void SubmitDeleteEvent(object sender, RoutedEventArgs e)
        {
            if (EventManager.Delete(eventId))
            {
                Close();
                using (var context = new MyDBContext())
                {
                    MyEvents.myDataGrid.ItemsSource = context.Events.Where(e => e.UserId == UserService.Instance.User.Id).ToList(); // Update de datagrid
                }
                MessageBox.Show("Event succesfully deleted");
            }
        }
    }
}