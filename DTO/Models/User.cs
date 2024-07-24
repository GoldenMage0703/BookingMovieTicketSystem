using System;
using System.Collections.Generic;

namespace DTO.Models
{
    public partial class User
    {
        public User()
        {
            Payments = new HashSet<Payment>();
        }

        public int UserId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Address { get; set; } = null!;
        public DateTime RegistrationDate { get; set; }
        public int? RoleId { get; set; }

        public virtual Role? Role { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
