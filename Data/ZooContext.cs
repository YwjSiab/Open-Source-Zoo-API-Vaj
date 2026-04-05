using Microsoft.EntityFrameworkCore;
using NTC_Zoo_API.Models;

namespace NTC_Zoo_API.Data
{
    public class ZooContext : DbContext
    {
        public ZooContext(DbContextOptions<ZooContext> options) : base(options)
        {
        }

        public DbSet<Animal> Animals { get; set; }
    }
}