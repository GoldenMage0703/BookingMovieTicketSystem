using DTO.Lib;
using DTO.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.Repositories;

namespace Web.Pages.Managers.Movies
{
    public class ManageMovieModel : PageModel
    {
        private readonly IRepository<Movie> _repository;
        public IList<Movie> Movie { get; set; }
        public ManageMovieModel(IRepository<Movie> repository)
        {
            _repository = repository;
        }
        public async Task OnGet()
        {
            Movie = await _repository.getAll();
        }
        public async Task<IActionResult> OnPost()
        {
            string fileName = "Movies.xlsx";
            string filePath = Path.Combine(Path.GetTempPath(), fileName);
            ExportTemplate.ExportMoviesToExcel(filePath);

            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }

    }
}
