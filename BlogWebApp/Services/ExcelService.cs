using BlogWebApp.Interfaces;
using BlogWebApp.Models;
using OfficeOpenXml;
using System.IO;
using System.Linq;

public class ExcelService : IExcelService
{
    public byte[] GenerateExcelFile(IEnumerable<Post> posts)
    {
        using (var package = new ExcelPackage())
        {
            var worksheet = package.Workbook.Worksheets.Add("Posts");

            worksheet.Cells[1, 1].Value = "Nr";
            worksheet.Cells[1, 2].Value = "Id";
            worksheet.Cells[1, 3].Value = "Date";
            worksheet.Cells[1, 4].Value = "Title";
            worksheet.Cells[1, 5].Value = "Description";
            worksheet.Cells[1, 6].Value = "Category";
            worksheet.Cells[1, 7].Value = "Full Name";

            int row = 2;
            foreach (var post in posts)
            {
                worksheet.Cells[row, 1].Value = row - 1;
                worksheet.Cells[row, 2].Value = post.Id;
                worksheet.Cells[row, 3].Value = post.CreatedDate.ToString("yyyy-MM-dd");
                worksheet.Cells[row, 4].Value = post.Title;
                worksheet.Cells[row, 5].Value = post.Description;
                worksheet.Cells[row, 6].Value = string.Join(", ", post.Category);
                worksheet.Cells[row, 7].Value = post.AppUser?.FullName;

                row++;
            }

            return package.GetAsByteArray();
        }
    }
}
