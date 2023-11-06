using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(40)]
        [MinLength(2)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(40)]
        [MinLength(2)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(6)]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [Required]
        [MaxLength(40)]
        [MinLength(5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [MaxLength(16)]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        public ICollection<Event> Events { get; }
        public ICollection<Subscription> Subscriptions { get; }
    }
}
