using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DisplayObject
{
    public class SeatStatus
    {
        public int SeatId { get; set; }
        public int TheaterId { get; set; }
        public string RowNum { get; set; }
        public int SeatNum { get; set; }
        public bool? Available { get; set; }
        public decimal? Price { get; set; }
        public bool IsBooking { get; set; } // True if booked, False if not
    }
}
