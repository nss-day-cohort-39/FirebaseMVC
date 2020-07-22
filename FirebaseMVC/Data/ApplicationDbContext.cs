using FirebaseMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace FirebaseMVC.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<UserProfile> UserProfile { get; set; }
    }
}
