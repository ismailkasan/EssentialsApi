using Essentials.Domain;
using Microsoft.EntityFrameworkCore;

namespace Essentials.Data
{
    public class EssentialsEntities(DbContextOptions<EssentialsDbContext> options) : DbContext(options)
    {
        public DbSet<Product> Product { get; set; }
    }
}
