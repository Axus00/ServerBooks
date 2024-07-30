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
        public DbSet<Models.Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookBorrow> BookBorrows { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserData> UserDatas { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasMany(u => u.UserDatas)
                .WithOne(ud => ud.User)
                .HasForeignKey(ud => ud.UserId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.UserRoles)
                .WithOne(ur => ur.User)
                .HasForeignKey(ur => ur.UserId);

            // Configuración de mapeo para el Enum StatusEnum en la tabla Coupons
            modelBuilder.Entity<Models.Author>()
                .Property(e => e.Status)
                .HasConversion<string>();
        
            modelBuilder.Entity<Book>()
                .Property(e => e.Status)
                .HasConversion<string>();

            modelBuilder.Entity<BookBorrow>()
                .Property(e => e.BorrowStatus)
                .HasConversion<string>();
        }
        
        /* protected override void OnModelCreating(ModelBuilder modelBuilder)
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

            // Otras configuraciones si es necesario
        }
    }

    
}