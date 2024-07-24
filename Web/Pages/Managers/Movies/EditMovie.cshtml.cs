using DTO.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Managers.Movies
{
    public class EditMovieModel : PageModel
    {
        private readonly PRN221_ProjectContext _projectContext;
        public EditMovieModel(PRN221_ProjectContext context)
        {
            _projectContext = context;
        }
        [BindProperty]
        public Movie movie { get; set; }
        public void OnGet(int? moviedID)
        {
            movie = _projectContext.Movies.FirstOrDefault(x => x.MovieId == moviedID);
        }
        public IActionResult onPost()
        {

            return Page();
        }
    }
}
