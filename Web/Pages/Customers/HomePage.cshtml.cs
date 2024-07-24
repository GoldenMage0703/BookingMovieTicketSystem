using DTO.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories;


namespace Web.Pages.Customers
{
    public class MovieListModel : PageModel
    {
        private readonly IRepository<Movie> _repository;

        private readonly PRN221_ProjectContext _projectContext;

        public IList<Movie> Movie { get; set; }
        public MovieListModel(IRepository<Movie> repository, PRN221_ProjectContext projectContext)
        {
            _repository = repository;
            _projectContext = projectContext;
        }
        public async Task OnGet()
        {
           
            Movie = await _projectContext.Movies.Take(8).ToListAsync();
        }
    }
}
