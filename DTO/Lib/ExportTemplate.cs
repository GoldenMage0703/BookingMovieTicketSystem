using DTO.Models;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;

namespace DTO.Lib
{
    public class ExportTemplate
    {
        public static void ExportMoviesToExcel(string filePath)
        {
            // Set the license context
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Movies");

                // Add headers
                worksheet.Cells[1, 1].Value = "Title";
                worksheet.Cells[1, 2].Value = "Release Date";
                worksheet.Cells[1, 3].Value = "Duration";
                worksheet.Cells[1, 4].Value = "Story Line";
                worksheet.Cells[1, 5].Value = "Poster";
                worksheet.Cells[1, 6].Value = "Director Name";
                worksheet.Cells[1, 7].Value = "Rating";
                worksheet.Cells[1, 8].Value = "Time";
                worksheet.Cells[1, 9].Value = "Trailer";

                // Add movie data
                // Example: worksheet.Cells[2, 1].Value = "Movie Title";

                // Format header row
                using (ExcelRange range = worksheet.Cells[1, 1, 1, 9])
                {
                    range.Style.Font.Bold = true;
                    range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                }

                // Save the package
                FileInfo fileInfo = new FileInfo(filePath);
                package.SaveAs(fileInfo);
            }
        }
    }
}
