using DTO.DisplayObject;
using DTO.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Repository.Repositories;
using System.Collections;
using Web.Hubs;
using Web.SessionExtensions;

namespace Web.Pages.Customers
{
    public class SeatSelectedModel : PageModel
    {
        private readonly PRN221_ProjectContext _projectContext;
        private readonly IRepository<Seat> _SeatRepository;
        private readonly IRepository<Showtime> _showtimeRepository;
        private readonly IHubContext<SignalRHub> _hubContext;

        public IList<SeatStatus> SelectedSeatList { get; set; }
        public IList<Showtime> ShowTimeList { get; set; }
        [BindProperty]
        public int showtimeInCart { get; set; }
        [BindProperty]
        public int[] seatIDInCart { get; set; }


        public SeatSelectedModel(IRepository<Seat> repository, PRN221_ProjectContext projectContext, IRepository<Showtime> showtime, IHubContext<SignalRHub> _hubContext)
        {
            _SeatRepository = repository;
            _projectContext = projectContext;
            _showtimeRepository = showtime;
            this._hubContext = _hubContext;
        }

        public async Task<IActionResult> OnGetAsync(string? movieID, string? theaterID)
        {
            if (movieID != null || theaterID != null)
            {
                var showtime = await _projectContext.Showtimes
                    .FirstOrDefaultAsync(x => x.MovieId == int.Parse(movieID) && x.TheaterId == int.Parse(theaterID));

                if (showtime != null)
                {
                    SelectedSeatList = await GetSeatsForShowtimeAsync(showtime.ShowtimeId);
                    ShowTimeList = await _projectContext.Showtimes
                        .Where(x => x.MovieId == int.Parse(movieID) && x.TheaterId == int.Parse(theaterID))
                        .ToListAsync();
                    showtimeInCart = showtime.ShowtimeId;

                    return Page();
                }
            }

            return RedirectToPage("MovieList");
        }

        public async Task<IActionResult> OnGetSeatByShowTimeAsync(int showtimeIDtoGetSeat)
        {
            SelectedSeatList = await GetSeatsForShowtimeAsync(showtimeIDtoGetSeat);
            return Partial("_seatPartial", SelectedSeatList);
        }

        private async Task<List<SeatStatus>> GetSeatsForShowtimeAsync(int showtimeId)
        {
            var bookedSeats = await _projectContext.Bookings
                .Where(b => b.ShowtimeId == showtimeId)
                .Select(b => b.SeatId)
                .ToListAsync();

            var seatsWithBookingStatus = await _projectContext.Seats
                .Join(
                    _projectContext.Showtimes,
                    seat => seat.TheaterId,
                    showtime => showtime.TheaterId,
                    (seat, showtime) => new { seat, showtime }
                )
                .Where(joined => joined.showtime.ShowtimeId == showtimeId)
                .Select(joined => new SeatStatus
                {
                    SeatId = joined.seat.SeatId,
                    TheaterId = joined.seat.TheaterId,
                    RowNum = joined.seat.RowNum,
                    SeatNum = joined.seat.SeatNum,
                    Available = joined.seat.Available,
                    Price = joined.seat.Price,
                    IsBooking = bookedSeats.Contains(joined.seat.SeatId)
                })
                .OrderBy(seat => seat.RowNum)
                .ThenBy(seat => seat.SeatNum)
                .ToListAsync();

            return seatsWithBookingStatus;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAddToCartAsync()
        {
            var sessiondata = HttpContext.Session.GetComplexData<Dictionary<int,ItemCart>>("ItemCart");
            if (sessiondata !=null)
            {
                if (seatIDInCart != null && seatIDInCart != null)
                {
                    foreach (var item in seatIDInCart)
                    {
                        if (sessiondata.ContainsKey(item + showtimeInCart) == false)
                        {
                            sessiondata.Add(item + showtimeInCart, new ItemCart { Seat = await _SeatRepository.getSelected(item), Showtime = await _showtimeRepository.GetShowtimeById(showtimeInCart) });
                        }
                        
                    }
                    HttpContext.Session.SetComplexData("ItemCart", sessiondata);
                    await _hubContext.Clients.All.SendAsync("cart");
                    return RedirectToPage("MyCart");

                }
            }
            else
            {
                if (seatIDInCart != null && seatIDInCart != null)
                {
                    Dictionary<int,ItemCart> itemCarts = new Dictionary<int, ItemCart>();
                    foreach (var item in seatIDInCart)
                    {
                        if (itemCarts.ContainsKey(showtimeInCart + item) == false)
                        {
                            itemCarts.Add(item + showtimeInCart, new ItemCart { Seat = await _SeatRepository.getSelected(item), Showtime = await _showtimeRepository.GetShowtimeById(showtimeInCart) });

                        }
                    }
                    if (itemCarts != null)
                    {

                        HttpContext.Session.SetComplexData("ItemCart", itemCarts);
                    }
                    await _hubContext.Clients.All.SendAsync("cart");
                    return RedirectToPage("MyCart");

                }
            }
         
            return Page();

        }
    }
}
