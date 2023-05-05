using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MODEL.Entity;

namespace MODEL
{
    public class E_BookDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public E_BookDbContext(DbContextOptions<E_BookDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            SeedRoles(builder);
            SeedContact(builder);
            SeedReview(builder);
        }
        public static void SeedRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole() { Name = "ADMIN", ConcurrencyStamp = "1", NormalizedName = "ADMIN" },
                new IdentityRole() { Name = "USER", ConcurrencyStamp = "2", NormalizedName = "USER" });
        }
        public static void SeedContact(ModelBuilder modelBuilder)
        {
            var userId = "9408eccf-0b8b-4d88-b951-e10f83198e18";
            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    Id = 1,
                    AppUserId = userId,
                    Title = "The Alchemist",
                    Author = "Paulo Coelho",
                    NoPage = 208,
                    Description = "The story of Santiago, an Andalusian shepherd boy...",
                    ISBN = "978-0062315007",
                    AddedToLib = DateTime.Now,
                    BookImg ="",
                    Publisher = "HarperOne",
                    PublisherDate = new DateTime(1988, 4, 25),
                    
                },
                new Book
                {
                    Id = 2,
                    AppUserId=userId,
                    Title = "To Kill a Mockingbird",
                    Author = "Harper Lee",
                    NoPage = 336,
                    BookImg = "",
                    Description = "The story of a young girl and her lawyer father...",
                    ISBN = "978-0446310789",
                    AddedToLib = DateTime.Now,
                    Publisher = "Grand Central Publishing",
                    PublisherDate = new DateTime(1960, 7, 11),
                    ViewBook = 0
                },
                 new Book
                 {
                     Id = 3,
                     AppUserId=userId,
                     Title = "1984",
                     Author = "George Orwell",
                     NoPage = 328,
                     BookImg = "",
                     Description = "A dystopian novel set in a totalitarian society...",
                     ISBN = "978-0451524935",
                     AddedToLib = DateTime.Now,
                     Publisher = "Signet Classics",
                     PublisherDate = new DateTime(1949, 6, 8),
                     ViewBook = 0
                 }
                
                );
        }
        public static void SeedReview(ModelBuilder modelBuilder)
        {
            var userId = "9408eccf-0b8b-4d88-b951-e10f83198e18";
            modelBuilder.Entity<Review>().HasData(

                new Review() { AppUserId=userId,BookId=3,Comment="The book was lengthy but I loved it",Rating=4,Title="Golden boy",DateCreated=DateTime.Now,Id=1},
                new Review() { AppUserId = userId, BookId = 2, Comment = "The books were lengthy but I loved it", Rating = 3, Title = "Golden girl", DateCreated = DateTime.Now, Id = 2 },
                new Review() { AppUserId = userId, BookId = 2, Comment = "The book was lengthy but I loved it", Rating = 4, Title = "Golden woman", DateCreated = DateTime.Now, Id = 3 }

                );
        }
    }
}
