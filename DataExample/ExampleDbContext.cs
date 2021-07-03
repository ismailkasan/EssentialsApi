// <copyright file="ExampleDbContext.cs" company="TC Sağlık Bakanlığı">
// Copyright(c) 2021 Tüm Hakları Saklıdır
// </copyright>
// <author>İsmail KAŞAN</author>
// <date>1.07.2021</date>

using Microsoft.EntityFrameworkCore;

namespace DataExample
{
    /// <summary>
    /// Defines the <see cref="ExampleDbContext" />.
    /// </summary>
    public class ExampleDbContext : Entities
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExampleDbContext"/> class.
        /// </summary>
        /// <param name="options">The options<see cref="DbContextOptions{ExampleDbContext}"/>.</param>
        public ExampleDbContext(DbContextOptions<ExampleDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// The OnModelCreating.
        /// </summary>
        /// <param name="builder">The builder<see cref="ModelBuilder"/>.</param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
