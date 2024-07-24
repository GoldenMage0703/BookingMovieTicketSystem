using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.Drawing.Chart;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DTO.Models;

namespace YourNamespace.Pages
{
    public class IndexModel : PageModel
    {
        private readonly PRN221_ProjectContext _context;

        public IndexModel(PRN221_ProjectContext context)
        {
            _context = context;
        }

        public decimal? TotalAmount { get; set; }
        public string TheaterLabelsJson { get; set; }
        public string TheaterPaymentsJson { get; set; }
        public string MovieLabelsJson { get; set; }
        public string MoviePaymentsJson { get; set; }

        public void OnGet()
        {
            // Calculate total payments
            TotalAmount = _context.Payments.Sum(p => p.TotalAmount);

            // Get payments by theater
            var theaterPayments = _context.Seats
                .Join(_context.Bookings, seat => seat.SeatId, booking => booking.SeatId, (seat, booking) => new { seat.TheaterId, booking.PaymentId })
                .Join(_context.Payments, combined => combined.PaymentId, payment => payment.PaymentId, (combined, payment) => new { combined.TheaterId, payment.TotalAmount })
                .GroupBy(x => x.TheaterId)
                .Select(g => new
                {
                    TheaterId = g.Key,
                    TotalAmount = g.Sum(x => x.TotalAmount)
                })
                .ToList();

            var theaters = _context.Theaters.ToDictionary(t => t.TheatreId, t => t.Name);
            var theaterLabels = theaterPayments.Select(tp => theaters.ContainsKey(tp.TheaterId) ? theaters[tp.TheaterId] : "Unknown Theater").ToList();
            var theaterAmounts = theaterPayments.Select(tp => tp.TotalAmount).ToList();

            // Convert to JSON format for Chart.js
            TheaterLabelsJson = JsonConvert.SerializeObject(theaterLabels);
            TheaterPaymentsJson = JsonConvert.SerializeObject(theaterAmounts);

            // Get payments by movie
            var moviePayments = _context.Bookings
                .Join(_context.Payments, booking => booking.PaymentId, payment => payment.PaymentId, (booking, payment) => new { booking.Showtime.MovieId, payment.TotalAmount })
                .GroupBy(x => x.MovieId)
                .Select(g => new
                {
                    MovieId = g.Key,
                    TotalAmount = g.Sum(x => x.TotalAmount)
                })
                .ToList();

            var movies = _context.Movies.ToDictionary(m => m.MovieId, m => m.Title);
            var movieLabels = moviePayments.Select(mp => movies.ContainsKey(mp.MovieId) ? movies[mp.MovieId] : "Unknown Movie").ToList();
            var movieAmounts = moviePayments.Select(mp => mp.TotalAmount).ToList();

            // Convert to JSON format for Chart.js
            MovieLabelsJson = JsonConvert.SerializeObject(movieLabels);
            MoviePaymentsJson = JsonConvert.SerializeObject(movieAmounts);
        }

        public IActionResult OnPostExport(string theaterLabelsJson, string theaterPaymentsJson, string movieLabelsJson, string moviePaymentsJson)
        {
            // Deserialize the JSON strings
            var theaterLabels = JsonConvert.DeserializeObject<List<string>>(theaterLabelsJson);
            var theaterPayments = JsonConvert.DeserializeObject<List<decimal>>(theaterPaymentsJson);
            var movieLabels = JsonConvert.DeserializeObject<List<string>>(movieLabelsJson);
            var moviePayments = JsonConvert.DeserializeObject<List<decimal>>(moviePaymentsJson);

            using (var package = new ExcelPackage())
            {
                // Create a worksheet for theater payments
                var theaterWorksheet = package.Workbook.Worksheets.Add("Theater Payments");
                theaterWorksheet.Cells[1, 1].Value = "Theater";
                theaterWorksheet.Cells[1, 2].Value = "Total Payments";

                for (int i = 0; i < theaterLabels.Count; i++)
                {
                    theaterWorksheet.Cells[i + 2, 1].Value = theaterLabels[i];
                    theaterWorksheet.Cells[i + 2, 2].Value = theaterPayments[i];
                }

                // Add pie chart for theater payments
                var theaterChart = theaterWorksheet.Drawings.AddChart("TheaterPaymentsChart", eChartType.Pie) as ExcelPieChart;
                theaterChart.Series.Add(theaterWorksheet.Cells[2, 2, theaterLabels.Count + 1, 2], theaterWorksheet.Cells[2, 1, theaterLabels.Count + 1, 1]);
                theaterChart.Title.Text = "Theater Payments";
                theaterChart.SetPosition(theaterLabels.Count + 3, 0, 0, 0);
                theaterChart.SetSize(400, 400);

                // Create a worksheet for movie payments
                var movieWorksheet = package.Workbook.Worksheets.Add("Movie Payments");
                movieWorksheet.Cells[1, 1].Value = "Movie";
                movieWorksheet.Cells[1, 2].Value = "Total Payments";

                for (int i = 0; i < movieLabels.Count; i++)
                {
                    movieWorksheet.Cells[i + 2, 1].Value = movieLabels[i];
                    movieWorksheet.Cells[i + 2, 2].Value = moviePayments[i];
                }

                // Add pie chart for movie payments
                var movieChart = movieWorksheet.Drawings.AddChart("MoviePaymentsChart", eChartType.Pie) as ExcelPieChart;
                movieChart.Series.Add(movieWorksheet.Cells[2, 2, movieLabels.Count + 1, 2], movieWorksheet.Cells[2, 1, movieLabels.Count + 1, 1]);
                movieChart.Title.Text = "Movie Payments";
                movieChart.SetPosition(movieLabels.Count + 3, 0, 0, 0);
                movieChart.SetSize(400, 400);

                var stream = new MemoryStream();
                package.SaveAs(stream);
                stream.Position = 0;
                var fileName = $"PaymentsSummary_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
                var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                return File(stream, contentType, fileName);
            }
        }
    }
}
