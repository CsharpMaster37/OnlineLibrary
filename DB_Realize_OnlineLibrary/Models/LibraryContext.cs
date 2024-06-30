using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DB_Realize_OnlineLibrary.Models
{
    public class LibraryContext : IdentityDbContext<Reader>
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Fine> Fines { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Condition> Conditions { get; set; }
        public DbSet<GroupReaders> GroupReaders { get; set; }
        public DbSet<HistoryItem> History { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Reader> Readers { get; set; }
        public LibraryContext(DbContextOptions<LibraryContext> options)
             : base(options)
        {
        }
    }
}
