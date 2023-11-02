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

    }
}
