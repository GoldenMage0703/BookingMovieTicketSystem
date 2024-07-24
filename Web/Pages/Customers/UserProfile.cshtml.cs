using DTO.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Web.SessionExtensions;
using Newtonsoft.Json;

namespace Web.Pages.Customers
{
    public class UserProfileModel : PageModel
    {
        private readonly PRN221_ProjectContext _projectContext;

        public UserProfileModel(PRN221_ProjectContext projectContext)
        {
            _projectContext = projectContext;
        }

        [BindProperty]
        public User User { get; set; }

        [BindProperty]
        public User UserUpdate { get; set; }
        public string previousPassword { get; set; }
        [BindProperty]
        public string newPass { get; set; }
        public IList<Payment> PaginatedPayments { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 3; // Items per page
        public int TotalPayments { get; set; }
        public int TotalPages => (int)Math.Ceiling(TotalPayments / (double)PageSize);

        public async Task OnGetAsync(int? pageNumber)
        {
            if (pageNumber.HasValue)
            {
                PageNumber = pageNumber.Value;
            }

            User = HttpContext.Session.GetComplexData<User>("user");
            previousPassword = User.Password;

            var paymentsQuery = GetPaymentsByUserId(User.UserId);

            TotalPayments = paymentsQuery.Count();

            PaginatedPayments = await paymentsQuery
                .Skip((PageNumber - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();
        }

        private IQueryable<Payment> GetPaymentsByUserId(int userId)
        {
            return _projectContext.Payments.Where(x => x.Userid == userId).AsQueryable().OrderByDescending(x => x.PaymentId);
        }

        public async Task<IActionResult> OnPostEditProfileAsync()
        {
            User = HttpContext.Session.GetComplexData<User>("user");

            var paymentsQuery = GetPaymentsByUserId(User.UserId);

            TotalPayments = paymentsQuery.Count();

            PaginatedPayments = await paymentsQuery
                .Skip((PageNumber - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();

            var userInDb = await _projectContext.Users.FindAsync(User.UserId);

            if (userInDb == null)
            {
                return NotFound();
            }

            userInDb.FirstName = UserUpdate.FirstName;
            userInDb.LastName = UserUpdate.LastName;
          
            userInDb.Phone = UserUpdate.Phone;
            userInDb.Address = UserUpdate.Address;

            _projectContext.Users.Update(userInDb);
            await _projectContext.SaveChangesAsync();

            HttpContext.Session.SetComplexData("user", userInDb);

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostChangePassProfileAsync()
        {
            User = HttpContext.Session.GetComplexData<User>("user");

            var paymentsQuery = GetPaymentsByUserId(User.UserId);

            TotalPayments = paymentsQuery.Count();

            PaginatedPayments = await paymentsQuery
                .Skip((PageNumber - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();

            var userUpdate = await _projectContext.Users.FirstOrDefaultAsync(x => x.UserId == User.UserId);
            userUpdate.Password = newPass;
            _projectContext.Users.Update(userUpdate);
            await _projectContext.SaveChangesAsync();

            return RedirectToPage();
        }

        public async Task<IActionResult> OnGetBookingDetailsAsync(int paymentId)
        {
            try
            {
                var bookingDetails = await _projectContext.Bookings
                    .Where(b => b.PaymentId == paymentId)
                    .Include(b => b.Seat)
                    .Include(b => b.Showtime).Include(x=>x.Showtime.Movie).Include(x=>x.Showtime.Theater)
                    .ToListAsync();

                if (bookingDetails == null)
                {
                    return NotFound();
                }
                string json = JsonConvert.SerializeObject(bookingDetails);
                return Content(json, "application/json");
            }
            catch (Exception ex)
            {
                // Log the exception and return a JSON error message
                // You might use your logging framework here
                return new JsonResult(new { error = ex.Message }) { StatusCode = StatusCodes.Status500InternalServerError };
            }
        }

    }
}
