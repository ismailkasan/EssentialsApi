using Microsoft.EntityFrameworkCore;

namespace Essentials.Data
{
    public class EssentialsDbContext : EssentialsEntities
    {
        public EssentialsDbContext(DbContextOptions<EssentialsDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
