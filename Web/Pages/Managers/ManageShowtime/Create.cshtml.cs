using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DTO.Models;

namespace Web.Pages.Managers.ManageShowtime
{
    public class CreateModel : PageModel
    {
        private readonly PRN221_ProjectContext _context;

        public CreateModel(PRN221_ProjectContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Showtime Showtime { get; set; } = default!;

        public IActionResult OnGet()
        {
            ViewData["MovieId"] = new SelectList(_context.Movies, "MovieId", "Title");
            ViewData["TheaterId"] = new SelectList(_context.Theaters, "TheatreId", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Validate that StartTime and EndTime are at least the next day
            if (Showtime.StartTime < DateTime.Now.Date.AddDays(1) || Showtime.EndTime < DateTime.Now.Date.AddDays(1))
            {
                ModelState.AddModelError(string.Empty, "Showtime must be scheduled for the next day or later.");
                return Page();
            }

            // Validate that EndTime is after StartTime
            if (Showtime.EndTime <= Showtime.StartTime)
            {
                ModelState.AddModelError(string.Empty, "End time must be after start time.");
                return Page();
            }

            // Add Showtime to the database
            _context.Showtimes.Add(Showtime);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
