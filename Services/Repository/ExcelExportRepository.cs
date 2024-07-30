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
            ICollection<BookBorrow> registers = _context.BookBorrows
            .Where(option => option.UserId == id)
            .Include(u => u.Books)
                .ThenInclude(b => b.Authors)
            .ToList();

            DataTable table = new DataTable();

            table.Columns.AddRange(new DataColumn[] {
                new DataColumn("Book Title"),
                new DataColumn("Book Author"),
                new DataColumn("StartDate"),
                new DataColumn("EndDate"),
            });

            foreach (var register in registers)
            {
                table.Rows.Add( register.Books!.Name, register.Books!.Authors!.Name, register.StartDate, register.EndDate);
            }

            using (XLWorkbook workbook = new XLWorkbook())
            {
                workbook.Worksheets.Add(table, "Borrowed Books");

                using (MemoryStream stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return content;
                }
            }
            
        }

        public byte[] ExportExcelAdmin()
        {
            ICollection<Book> books = _context.Books
            .Include(b => b.Authors)
            .ToList();

            ICollection<UserData> usersData = _context.UserDatas
            .Include(u => u.User)
            .ToList();

            DataTable tableBooks = new DataTable(); 
            DataTable tableUsers = new DataTable();
           
            tableBooks.Columns.AddRange(new DataColumn[] 
            {
                new DataColumn("Book Title"),
                new DataColumn("Book Author"), 
                new DataColumn("Books Quantities "), 
                new DataColumn("Books Status "), 
             });
            


            foreach (var register in books)
            {
                tableBooks.Rows.Add(register.Name, register.Authors!.Name, register.Quantity, register.Status);
            }

            tableUsers.Columns.AddRange(new DataColumn[] 
            {
                new DataColumn("User Name"),
                new DataColumn("User Email"),
                new DataColumn("User Phone"),
                new DataColumn("User Status")
            });

            foreach (var register in usersData)
            {
                tableUsers.Rows.Add(register.User!.Names, register.Email, register.Phone, register.User.Status);
            }

            using (XLWorkbook workbook = new XLWorkbook())
            {
                workbook.Worksheets.Add(tableUsers, "Customers");
                workbook.Worksheets.Add(tableBooks, "Books");

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