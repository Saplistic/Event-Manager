using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Model
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Description { get; set; } = "";

        [DataType(DataType.DateTime)]
        public DateTime StartTime { get; set; } = DateTime.Now;

        [DataType(DataType.DateTime)]
        public DateTime EndTime { get; set; }
    }
}
