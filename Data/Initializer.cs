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
            if (!dbContext.Events.Any())
            {
                dbContext
                    .Events.Add(new Event
                    {
                    Name = "Event 1",
                    StartTime = DateTime.Now,
                    EndTime = DateTime.Now,
                    Location = "Location 1",
                    Description = "Description 1"
                });
                dbContext
                    .Events.Add(new Event
                    {
                        Name = "Event 2",
                        StartTime = DateTime.Now,
                        EndTime = DateTime.Now,
                        Location = "Location 2",
                        Description = "Description 2"
                    });
                dbContext.Add(new Event
                {
                    Name = "Event 3",
                    StartTime = DateTime.Now,
                    EndTime = DateTime.Now,
                    Location = "Location 3",
                    Description = "Description 3"
                });

                dbContext.SaveChanges();
            }
        }
    }
}
