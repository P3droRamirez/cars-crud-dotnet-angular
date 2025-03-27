using CarsApi.Model;
using Microsoft.EntityFrameworkCore;

namespace CarsApi.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {

        }

        public DbSet<Coche> Coches { get; set; }
    }
}
