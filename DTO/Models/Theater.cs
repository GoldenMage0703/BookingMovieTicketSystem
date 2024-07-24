using Newtonsoft.Json;
using System;
using System.Collections.Generic;


namespace DTO.Models
{
    public partial class Theater
    {
        public Theater()
        {
            Seats = new HashSet<Seat>();
            Showtimes = new HashSet<Showtime>();
        }

        public int TheatreId { get; set; }
        public string Name { get; set; } = null!;
        public string Location { get; set; } = null!;
        public int Capacity { get; set; }
        [JsonIgnore]
        public virtual ICollection<Seat> Seats { get; set; }
        [JsonIgnore]
        public virtual ICollection<Showtime> Showtimes { get; set; }
    }
}
