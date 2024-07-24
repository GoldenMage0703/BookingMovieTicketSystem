using DTO.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Pages.Customers
{
    public class ListMovieModel : PageModel
    {
        private readonly PRN221_ProjectContext repository;
        public IList<Movie> Mov { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20; // Items per page
        public int TotalMovies { get; set; }
        public int TotalPages => (int)Math.Ceiling(TotalMovies / (double)PageSize);

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SortOrder { get; set; }

        public ListMovieModel(PRN221_ProjectContext repository)
        {
            this.repository = repository;
        }

        public async Task OnGetAsync(int? pageNumber, string searchString, string sortOrder)
        {
            if (pageNumber.HasValue)
            {
                PageNumber = pageNumber.Value;
            }

            SearchString = searchString;
            SortOrder = sortOrder;

            IQueryable<Movie> moviesQuery = repository.Movies;

            if (!string.IsNullOrEmpty(SearchString))
            {
                moviesQuery = moviesQuery.Where(m => m.Title.Contains(SearchString));
            }

            switch (SortOrder)
            {
                case "title_asc":
                    moviesQuery = moviesQuery.OrderBy(m => m.Title);
                    break;
                case "title_desc":
                    moviesQuery = moviesQuery.OrderByDescending(m => m.Title);
                    break;
                case "rating_asc":
                    moviesQuery = moviesQuery.OrderBy(m => m.Rating);
                    break;
                case "rating_desc":
                    moviesQuery = moviesQuery.OrderByDescending(m => m.Rating);
                    break;
                default:
                    moviesQuery = moviesQuery.OrderBy(m => m.Title);
                    break;
            }

            TotalMovies = await moviesQuery.CountAsync();

            Mov = await moviesQuery
                .Skip((PageNumber - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();
        }
    }
}
