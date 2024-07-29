using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Books.Services.Interface;
using Books.Models;

namespace Books.Services.Interface
{
    public interface IEmailRepository
    {
        Task SendEmailAsync(string toEmail, string subject, string body);
    }
}