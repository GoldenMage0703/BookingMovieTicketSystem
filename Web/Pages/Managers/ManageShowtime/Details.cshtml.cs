using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DTO.Models;

namespace Web.Pages.Managers.ManageShowtime
{
    public class DetailsModel : PageModel
    {
        private readonly DTO.Models.PRN221_ProjectContext _context;

        public DetailsModel(DTO.Models.PRN221_ProjectContext context)
        {
            _context = context;
        }

      public Showtime Showtime { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Showtimes == null)
            {
                return NotFound();
            }

            var showtime = await _context.Showtimes.Include(x=>x.Movie).Include(x=>x.Theater).FirstOrDefaultAsync(m => m.ShowtimeId == id);
            if (showtime == null)
            {
                return NotFound();
            }
            else 
            {
                Showtime = showtime;
            }
            return Page();
        }
    }
}
