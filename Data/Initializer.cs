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
            //if (!dbContext.MyModels.Any())
            //{
            //    dbContext.Add(new MyModel { Name = "model1" });
            //    dbContext.Add(new MyModel { Name = "model2" });
            //    dbContext.Add(new MyModel { Name = "model3" });

            //    dbContext.SaveChanges();
            //}
        }
    }
}
