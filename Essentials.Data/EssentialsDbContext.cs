using System;
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "Server=ismail-demo-db.database.windows.net;Database=ismail-demo-db;User ID=ismailAzureSqlAdmin;Password=HD45W4*F537rtK!;Trusted_Connection=False;Encrypt=True;";

            optionsBuilder.UseSqlServer(connectionString, retry =>
            {
                retry.ExecutionStrategy(s => new CustomExecutionStrategy(this, 10, TimeSpan.FromSeconds(15)));
            });
        }

      
    }
}
