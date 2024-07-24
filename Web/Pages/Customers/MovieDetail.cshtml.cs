using DTO.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories;
using Web.SessionExtensions;

namespace Web.Pages.Customers
{
    public class MovieDetailModel : PageModel
    {
        private readonly IRepository<Movie> _repository;
        private readonly IRepository<Theater> _theaterRepository;
        private readonly IRepository<Showtime> _showtimeRepository;
        private readonly PRN221_ProjectContext _projectContext;

        public MovieDetailModel(IRepository<Movie> repository, IRepository<Theater> theaterRepository, PRN221_ProjectContext projectContext)
        {
            _repository = repository;
            _theaterRepository = theaterRepository;
            _projectContext = projectContext;
        }

        public Movie Movie { get; set; }
        //    public IList<Theater> TheaterList { get; set; }
        public IList<Showtime> ShowtimeList { get; set; }
        [BindProperty]
        public int? TheaterGet { get; set; }
        [BindProperty]
        public User userToget { get; set; }
        public IList<DisplayTheater> TheaterList { get; set; }
        public async Task OnGetAsync(string? id)
        {
            userToget = HttpContext.Session.GetComplexData<User>("user");
            if (id != null)
            {

                // TheaterList = await _theaterRepository.getAll();
                var result = (from theater in _projectContext.Theaters
                              join showtime in _projectContext.Showtimes on theater.TheatreId equals showtime.TheaterId
                              where showtime.MovieId == int.Parse(id) && showtime.StartTime>DateTime.Now
                              group theater by new { theater.TheatreId, theater.Name } into g
                              select new DisplayTheater
                              {
                                  TheatreId = g.Key.TheatreId,
                                  Name = g.Key.Name
                              });
                TheaterList = result.ToList();
                Movie = await _repository.getSelected(int.Parse(id));


                if (Movie == null)
                {
                    // Redirect to an error page or display an error message
                    RedirectToPage("/Error");
                }
            }
            elsehttp://localhost:5017/Customers/SeatSelected?movieID=4
            {
                // Handle the case where the id is missing
                RedirectToPage("/Error");
            }
        }

        public async Task<IActionResult> OnPostViewSeat(int? movieIdToGet)
        {
            if (TheaterGet.HasValue)
            {
                return RedirectToPage("SeatSelected", new { movieID = movieIdToGet, theaterID = TheaterGet });
            }
            return Page();
        }

    }
    public class DisplayTheater
    {
        public int TheatreId { get; set; }
        public string Name { get; set; }
    }
}
