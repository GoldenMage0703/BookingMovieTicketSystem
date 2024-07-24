using System;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace DTO.Models
{
    public partial class Showtime
    {
        public Showtime()
        {
            Bookings = new HashSet<Booking>();
        }

        public int ShowtimeId { get; set; }
        public int MovieId { get; set; }
        public int TheaterId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public virtual Movie Movie { get; set; } = null!;
       
        public virtual Theater Theater { get; set; } = null!;
        [JsonIgnore]
        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
