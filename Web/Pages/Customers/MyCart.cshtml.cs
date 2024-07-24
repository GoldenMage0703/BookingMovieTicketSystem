using DTO.DisplayObject;
using DTO.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Web.Hubs;
using Web.SessionExtensions;

namespace Web.Pages.Customers
{
    public class MyCartModel : PageModel
    {
        private readonly PRN221_ProjectContext _projectContext;
        private readonly IHubContext<SignalRHub> _hubContext;

        public MyCartModel(PRN221_ProjectContext pRN221_ProjectContext, IHubContext<SignalRHub> _hubContext)
        {
            _projectContext = pRN221_ProjectContext;
            this._hubContext = _hubContext;
        }

        public Dictionary<int, ItemCart> ItemInCar { get; set; }

        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public string Phone { get; set; }

        [BindProperty]
        public decimal? Count { get; set; } = 0;

        public string SuccessMessage { get; set; }

        public void OnGet()
        {
            ItemInCar = HttpContext.Session.GetComplexData<Dictionary<int, ItemCart>>("ItemCart") ?? new Dictionary<int, ItemCart>();
            if (ItemInCar.Any())
            {
                foreach (var item in ItemInCar.Values)
                {
                    Count += item.Seat.Price;
                }
            }
        }

        public async Task<IActionResult> OnPostAddToCartAsync()
        {
            ItemInCar = HttpContext.Session.GetComplexData<Dictionary<int, ItemCart>>("ItemCart") ?? new Dictionary<int, ItemCart>();
            try
            {
                User userToget = HttpContext.Session.GetComplexData<User>("user");
                ItemInCar = HttpContext.Session.GetComplexData<Dictionary<int, ItemCart>>("ItemCart") ?? new Dictionary<int, ItemCart>();

                if (ItemInCar.Count == 0)
                {
                    ModelState.AddModelError("", "Your cart is empty.");
                    return Page();
                }

                if (ItemInCar.Any())
                {
                    foreach (var item in ItemInCar.Values)
                    {
                        Count += item.Seat.Price;
                    }
                }

                Payment pay = new Payment
                {
                    PaymentDate = DateTime.Now,
                    PaymentMethod = "1", // Adjust according to your payment methods
                    TotalAmount = Count,
                    Userid = userToget.UserId,
                };

                _projectContext.Payments.Add(pay);
                await _projectContext.SaveChangesAsync();

                foreach (var item in ItemInCar.Values)
                {
                    Booking booking = new Booking
                    {
                        BookingDate = pay.PaymentDate,
                        PaymentId = pay.PaymentId,
                        SeatId = item.Seat.SeatId,
                        CustomerName = Name,
                        CustomerPhone = Phone,
                        ShowtimeId = item.Showtime.ShowtimeId
                    };

                    _projectContext.Bookings.Add(booking);
                }

                await _projectContext.SaveChangesAsync();
                HttpContext.Session.Remove("ItemCart");
                await _hubContext.Clients.All.SendAsync("Checkout");
                SuccessMessage = "Item added to cart successfully!";
                return Page();
            }
            catch (FormatException ex)
            {
                ModelState.AddModelError("", "There was a format error with your input.");
                return Page();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while processing your request.");
                return Page();
            }
        }

        public async Task<IActionResult> OnPostRemoveFromCartAsync(int seatID, int showID)
        {
            // Retrieve the cart from session
            ItemInCar = HttpContext.Session.GetComplexData<Dictionary<int, ItemCart>>("ItemCart") ?? new Dictionary<int, ItemCart>();

            // Find the item with the specified identifier
            var itemToRemove = ItemInCar.Values.FirstOrDefault(item => item.Seat.SeatId + item.Showtime.ShowtimeId == seatID + showID);

            if (itemToRemove != null)
            {
                // Remove the item by its key
                var itemKey = ItemInCar.FirstOrDefault(x => x.Value.Seat.SeatId + x.Value.Showtime.ShowtimeId == seatID + showID).Key;
                ItemInCar.Remove(itemKey);

                // Update the session
                HttpContext.Session.SetComplexData("ItemCart", ItemInCar);

                // Optionally update the Count if needed
                Count -= itemToRemove.Seat.Price;
                await _hubContext.Clients.All.SendAsync("RemoveItemFromCart");
                return Page();
            }

            return Page();
        }
    }

}
