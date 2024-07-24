using DTO.Models;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Lib
{
    public class ImportFromExcell
    {
        public static List<Movie> ImportMoviesFromExcel(string filePath)
        {
            var movies = new List<Movie>();

            // Set the license context
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var worksheet = package.Workbook.Worksheets[0];
                int rowCount = worksheet.Dimension.Rows;
                int colCount = worksheet.Dimension.Columns;

                for (int row = 2; row <= rowCount; row++)
                {
                    var movie = new Movie
                    {
                        Title = worksheet.Cells[row, 1].Text,
                        ReleaseDate = DateTime.Parse(worksheet.Cells[row, 2].Text),
                        Duration = int.Parse(worksheet.Cells[row, 3].Text),
                        StoryLine = worksheet.Cells[row, 4].Text,
                        Poster = worksheet.Cells[row, 5].Text,
                        DirectorName = worksheet.Cells[row, 6].Text,
                        Rating = worksheet.Cells[row, 7].Text,
                        Time = worksheet.Cells[row, 8].Text,
                        Trailer = worksheet.Cells[row, 9].Text
                    };

                    movies.Add(movie);
                }
            }

            return movies;
        }
    }
}
