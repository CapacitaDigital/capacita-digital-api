using Microsoft.EntityFrameworkCore;
using UserContext.Models;

namespace SadContext.Models
{
    public class SadDbContext : DbContext
    {
        public SadDbContext(DbContextOptions<SadDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }

}
