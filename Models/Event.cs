﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(40)]
        [MinLength(6)]
        public string Name { get; set; }

        [Required]
        [MaxLength(60)]
        [MinLength(6)]
        public string Location { get; set; }

        [MaxLength(600)]
        public string Description { get; set; } = "";

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime StartTime { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime EndTime { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public ICollection<Subscription> Subscriptions { get; set; }

        public string Duration
        {
            get
            {
                TimeSpan duration = EndTime - StartTime;
                return duration.ToString(@"hh\:mm");
                //return duration = EndTime - StartTime;
            }
        }

        public string Publisher
        {
            get { return User.FirstName + " " + User.LastName; }
        }

        public int SubscriptionsCount
        {
            get { return Subscriptions.Count; }
        }
    }
}
