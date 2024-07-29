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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configuración de las relaciones
        modelBuilder.Entity<User>()
            .HasOne(u => u.UserDatas);


        modelBuilder.Entity<BookBorrow>()
            .HasOne(bb => bb.Users)
            .WithMany()
            .HasForeignKey(bb => bb.UserId);

        modelBuilder.Entity<BookBorrow>()
            .HasOne(bb => bb.Books)
            .WithMany()
            .HasForeignKey(bb => bb.BookId);

        modelBuilder.Entity<Book>()
            .HasOne(b => b.Authors)
            .WithMany(a => a.Books)
            .HasForeignKey(b => b.AuthorId);
    }
    }
}