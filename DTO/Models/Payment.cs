using System;
using System.Collections.Generic;

namespace DTO.Models
{
    public partial class Payment
    {
        public Payment()
        {
            Bookings = new HashSet<Booking>();
        }

        public int PaymentId { get; set; }
        public decimal? TotalAmount { get; set; }
        public string? PaymentMethod { get; set; }
        public DateTime? PaymentDate { get; set; }
        public int? Userid { get; set; }

        public virtual User? User { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
