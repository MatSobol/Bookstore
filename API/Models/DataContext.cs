using Microsoft.EntityFrameworkCore;
using SharedP.Books;
using DataSeeder;
using SharedP.Auth;

namespace API.Models
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .Property(p => p.Id )
                .IsRequired()
                .HasColumnType("decimal(8,0)");

            modelBuilder.Entity<Book>()
                .Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Book>()
                .Property(p => p.Author)
                .HasMaxLength(100);


            modelBuilder.Entity<Book>().HasData(BookSeeder.GenerateBookData());
        }
    }
}
