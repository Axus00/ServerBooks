using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Services.Interface
{
    public interface IExcelExportRepository
    {
        byte[] ExportExcelCustomer(int id); //SELECT * FROM 

    }
}