using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Books.Models;
using DocumentFormat.OpenXml.Bibliography;


namespace Books.Infrastructure.Data
{
    public class BaseContext : DbContext
    {
        public BaseContext(DbContextOptions<BaseContext> options) : base(options)
        {

        }

        //Models
        public DbSet<User> Users { get; set; }
        public DbSet<Autor> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookBorrow> BookBorrows { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserData> UserDatas { get; set; }
        public DbSet<UserRole> UserRole { get; set; }

    }
}