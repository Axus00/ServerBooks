using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Books.Services.Interface;

namespace Books.Models
{
    public class EmailSettings
    {
        public string? SmtpServer { get; set; }
        public int Port { get; set; }
        public string? SenderName { get; set; }
        public string? SenderEmail { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}
