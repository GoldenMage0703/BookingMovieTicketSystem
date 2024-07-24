using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DTO.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;

namespace YourNamespace.Pages.ManageMovie
{
    public class IndexModel : PageModel
    {
        private readonly PRN221_ProjectContext _context;

        public IndexModel(PRN221_ProjectContext context)
        {
            _context = context;
        }
       
        public IList<Movie> Movies { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 8; // Items per page
        public int TotalItems { get; set; }
        public int TotalPages => (int)Math.Ceiling(TotalItems / (double)PageSize);

        public async Task OnGetAsync(int? pageNumber)
        {
            if (pageNumber.HasValue)
            {
                PageNumber = pageNumber.Value;
            }
            IQueryable<Movie> moviesQuery = _context.Movies;
            TotalItems = await _context.Movies.CountAsync();

            Movies = await moviesQuery
                 .Skip((PageNumber - 1) * PageSize)
                 .Take(PageSize)
                 .ToListAsync();
        }
    }
}
