using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace DTO.Models
{
    public partial class Seat
    {
        public Seat()
        {
            Bookings = new HashSet<Booking>();
        }

        public int SeatId { get; set; }
        public int TheaterId { get; set; }
        public string? RowNum { get; set; }
        public int SeatNum { get; set; }
        public bool? Available { get; set; }
        public decimal? Price { get; set; }
        [JsonIgnore]
        public virtual Theater Theater { get; set; } = null!;
        [JsonIgnore]
        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
