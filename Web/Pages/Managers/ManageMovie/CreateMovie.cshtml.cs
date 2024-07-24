using DTO.Lib;
using DTO.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Repository.Repositories;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Web.Hubs;
using Web.SessionExtensions;

namespace Web.Pages.Managers.ManageMovie
{
    public class CreateMovieModel : PageModel
    {
        private readonly PRN221_ProjectContext context;
        private readonly IHubContext<SignalRHub> _hubContext;
        public CreateMovieModel(PRN221_ProjectContext context, IHubContext<SignalRHub> _hubContext)
        {
            this.context = context;
            this._hubContext = _hubContext;
        }
        public IList<Movie> Movies { get; set; } = new List<Movie>();

        [BindProperty]
        public IFormFile ExcelFile { get; set; }

        public void OnGet()
        {
        }


        public async Task<IActionResult> OnPostUploadXlssAsync()
        {
            if (ExcelFile == null || ExcelFile.Length == 0)
            {
                return Content("File not selected or empty");
            }

            var filePath = Path.Combine(Path.GetTempPath(), ExcelFile.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await ExcelFile.CopyToAsync(stream);
            }

            var movies = ImportFromExcell.ImportMoviesFromExcel(filePath);

            foreach (var movie in movies)
            {
                Movies.Add(movie);
            }

            HttpContext.Session.SetComplexData("movieToAdd", Movies);
            return Page();
        }
        public async Task<IActionResult> OnPostInsertAllAsync()
        {
            var movies = HttpContext.Session.GetComplexData<List<Movie>>("movieToAdd");
            context.Movies.AddRange(movies);
            context.SaveChanges();
            await _hubContext.Clients.All.SendAsync("plspls");
            await _hubContext.Clients.All.SendAsync("addMovie");
            return RedirectToPage("./Index");
        }
        public async Task<IActionResult> OnPostAsync()
        {
            string fileName = "Movies.xlsx";
            string filePath = Path.Combine(Path.GetTempPath(), fileName);
            ExportTemplate.ExportMoviesToExcel(filePath);

            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }
    }
}
