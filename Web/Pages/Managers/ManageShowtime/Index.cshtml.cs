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
    public class IndexModel : PageModel
    {
        private readonly DTO.Models.PRN221_ProjectContext _context;

        public IndexModel(DTO.Models.PRN221_ProjectContext context)
        {
            _context = context;
        }
        [BindProperty(SupportsGet = true)]
        public DateTime date { get; set; }
        public IList<Showtime> Showtime { get;set; } = default!;

        public async Task OnGetAsync()
        {
            date = DateTime.Now;
            if (_context.Showtimes != null)
            {
                Showtime = await _context.Showtimes
                .Include(s => s.Movie)
                .Include(s => s.Theater).ToListAsync();
            }
        }
    }
}
