using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DTO.Models
{
    public partial class Booking
    {
        public int? ShowtimeId { get; set; }
        public int? SeatId { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerPhone { get; set; }
        public DateTime? BookingDate { get; set; }
        public int? PaymentId { get; set; }
        public int BookingId { get; set; }
        [JsonIgnore]
        public virtual Payment? Payment { get; set; }
        public virtual Seat? Seat { get; set; }
        public virtual Showtime? Showtime { get; set; }
    }
}
