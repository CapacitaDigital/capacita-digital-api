using Microsoft.EntityFrameworkCore;
using UserContext.Models;

namespace UserContext.Models
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User { 
                    Id = 1, 
                    Username = "admin", 
                    Password = "admin",
                   }
            );
        }
    }

}
