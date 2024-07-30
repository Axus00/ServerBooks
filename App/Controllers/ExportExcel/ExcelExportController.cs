using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Books.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ServerBooks.App.Controllers.ExportExcel
{
    [ApiController]
    [Route("api/[controller]")] 
    public class ExcelExportController  : ControllerBase
    {
        private readonly IExcelExportRepository _repository;

        public ExcelExportController(IExcelExportRepository repository)
        {
            _repository = repository;
        }

        //Method to download Excel file with the data of customer
        [HttpGet("{id}")]
        public IActionResult GetAllBooks(int id)
        {
            var download = _repository.ExportExcelCustomer(id);
            return File(download, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Borrowed Books.xlsx");
        }
    
        
    }
}