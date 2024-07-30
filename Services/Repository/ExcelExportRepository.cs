using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Books.Infrastructure.Data;
using Books.Models;
using Books.Services.Interface;
using ClosedXML.Excel;
using Microsoft.EntityFrameworkCore;

namespace Books.Services.Repository
{
    public class ExcelExportRepository : IExcelExportRepository
    {
        private readonly BaseContext _context;

        public ExcelExportRepository(BaseContext context)
        {
            _context = context;
        }

        public byte[] ExportExcelCustomer(int id)
        {
            //Obtain the loans by customer id
            ICollection<BookBorrow> registers = _context.BookBorrows
            .Where(option => option.UserId == id)
            .Include(u => u.Books)
             .ThenInclude(b => b.Authors)
            .ToList();

            //Create a new DataTable to store the data
            DataTable table = new DataTable();

            //Add columns to the DataTable
            table.Columns.AddRange(new DataColumn[] {
                new DataColumn("Book Title"),
                new DataColumn("Book Author"),
                new DataColumn("StartDate"),
                new DataColumn("EndDate"),
            });

            //Fill the DataTable with the data
            foreach (var register in registers)
            {
                table.Rows.Add( register.Books!.Name, register.Books!.Authors!.Name, register.StartDate, register.EndDate);
            }

            //Create a new Excel workbook and add the DataTable to it
            using (XLWorkbook workbook = new XLWorkbook())
            {
                //Create the sheet with the data
                workbook.Worksheets.Add(table, "Borrowed Books");

                //Save the Excel file to a memory stream and return it as a byte array
                using (MemoryStream stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return content;
                }
            }
            
        }

        

    }
}