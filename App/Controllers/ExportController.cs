using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Books.Models;
using ClosedXML.Excel;
using Books.Infrastructure.Data;
using DocumentFormat.OpenXml.Math;
using Microsoft.EntityFrameworkCore;


namespace Books.App.Controllers
{
    public class ExportController : ControllerBase
    {
        private readonly BaseContext _context;

        public ExportController(BaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> ExportUserBooksToExcel(int id)
        {


            var user = await _context.Users
                .Include(u => u.BookBorrows)
                .ThenInclude(bb => bb.Books)
                .ThenInclude(b => b.Authors)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            // Obtener los libros del usuario
            var books = user.BookBorrows.Select(bb => bb.Books).ToList();

            var workbook = new XLWorkbook();
            
                var worksheet = workbook.Worksheets.Add("Books");

                worksheet.Cell(1, 1).Value = "Book Name";
                worksheet.Cell(1, 2).Value = "Author Name";

                var currentRow = 1;
                foreach (var book in books)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = book.Name;
                    worksheet.Cell(currentRow, 2).Value = book.Authors?.Name ?? "Unknown";
                }

                // Guardar el archivo en un MemoryStream y devolverlo
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"User_{id}_Books.xlsx");
                }
        }
    }


    
}