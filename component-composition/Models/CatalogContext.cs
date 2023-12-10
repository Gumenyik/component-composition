using Microsoft.EntityFrameworkCore;

namespace component_composition.Models
{
    public class CatalogContext : DbContext
    {
        public CatalogContext(DbContextOptions<CatalogContext> options) : base(options)
        {
        }

        public DbSet<Aggregate> Aggregates { get; set; }
        public DbSet<State> States { get; set; }

  
    }
}
