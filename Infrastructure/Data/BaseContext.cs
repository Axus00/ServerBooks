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
            // Configuración de las relaciones entre User y UserRole
            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRole)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany()
                .HasForeignKey(ur => ur.RoleId);

            // // Configuración de las relaciones entre User y UserData
            // modelBuilder.Entity<User>()
            //     .HasOne(u => u.UserData)
            //     .WithOne(ud => ud.User)
            //     .HasForeignKey<UserData>(ud => ud.UserId);

            // Configuración de las relaciones entre BookBorrow, User y Book
            modelBuilder.Entity<BookBorrow>()
                .HasOne(bb => bb.Users)
                .WithMany(u => u.BookBorrows)
                .HasForeignKey(bb => bb.UserId);

            modelBuilder.Entity<BookBorrow>()
                .HasOne(bb => bb.Books)
                .WithMany()
                .HasForeignKey(bb => bb.BookId);

            // Configuración de las relaciones entre Book y Autor
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Authors)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId);
        }

    }
}