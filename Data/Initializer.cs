using Administration;
using EventManager.Model;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Ribbon.Primitives;

namespace EventManager.Data
{
    internal static class Initializer
    {
        internal static void DbSetInitializer(MyDBContext dbContext)
        {
            if (!dbContext.Users.Any())
            {
                dbContext.Users.Add(new User
                {
                    FirstName = "Aspen",
                    LastName = "Townsend",
                    EmailAddress = "litora.torquent@yahoo.ca",
                    Password = "user1",
                    PhoneNumber = "183-5525",
                });
                dbContext.Users.Add(new User
                {
                    FirstName = "Benedict",
                    LastName = "Middleton",
                    EmailAddress = "feugiat.non@google.org",
                    Password = "user2",
                    PhoneNumber = "491-5944"
                });
                dbContext.Users.Add(new User
                {
                    FirstName = "Levi",
                    LastName = "Waller",
                    EmailAddress = "sollicitudin.orci.sem@aol.edu",
                    Password = "user3",
                    PhoneNumber = "466-3321"
                });
                dbContext.Users.Add(new User
                {
                    FirstName = "Tasha",
                    LastName = "Hansen",
                    EmailAddress = "vestibulum.mauris.magna@hotmail.edu",
                    Password = "user4",
                    PhoneNumber = "1-686-511-600"
                });
                dbContext.Users.Add(new User
                {
                    FirstName = "Marvin",
                    LastName = "Phelps",
                    EmailAddress = "natoque@aol.edu",
                    Password = "user5",
                    PhoneNumber = "811-0532"
                });
                dbContext.SaveChanges();
            }

            if (!dbContext.Events.Any())
            {
                dbContext.Events.Add(new Event
                {
                    Name = "Event 1",
                    StartTime = DateTime.Now.AddDays(5),
                    EndTime = DateTime.Now.AddDays(5).AddHours(2),
                    Location = "Location 1",
                    Description = "Description 1",
                    UserId = 1
                });
                dbContext.Events.Add(new Event
                {
                    Name = "Event 2",
                    StartTime = DateTime.Now.AddDays(2),
                    EndTime = DateTime.Now.AddDays(2).AddHours(2),
                    Location = "Location 2",
                    Description = "Description 2",
                    UserId = 1
                });
                dbContext.Events.Add(new Event
                {
                    Name = "Event 3",
                    StartTime = DateTime.Now.AddHours(2),
                    EndTime = DateTime.Now.AddHours(3),
                    Location = "Location 3",
                    Description = "Description 3",
                    UserId = 2
                });

                dbContext.SaveChanges();
            }
        }
    }
}
