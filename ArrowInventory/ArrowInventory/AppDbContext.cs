using ArrowInventory.Models;
using Microsoft.EntityFrameworkCore;

namespace ArrowInventory.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Devices> Devices { get; set;  }
        public DbSet<Sites> Sites { get; set;  }


    }
}
