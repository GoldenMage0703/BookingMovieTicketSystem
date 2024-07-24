using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DTO.Models;

namespace Web.Pages.Managers.ManageShowtime
{
    public class EditModel : PageModel
    {
        private readonly DTO.Models.PRN221_ProjectContext _context;

        public EditModel(DTO.Models.PRN221_ProjectContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Showtime Showtime { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Showtimes == null)
            {
                return NotFound();
            }

            var showtime =  await _context.Showtimes.FirstOrDefaultAsync(m => m.ShowtimeId == id);
            if (showtime == null)
            {
                return NotFound();
            }
            Showtime = showtime;
           ViewData["MovieId"] = new SelectList(_context.Movies, "MovieId", "Rating");
           ViewData["TheaterId"] = new SelectList(_context.Theaters, "TheatreId", "Location");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Showtime).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShowtimeExists(Showtime.ShowtimeId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ShowtimeExists(int id)
        {
          return (_context.Showtimes?.Any(e => e.ShowtimeId == id)).GetValueOrDefault();
        }
    }
}
