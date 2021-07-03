// <copyright file="Entities.cs" company="TC Sağlık Bakanlığı">
// Copyright(c) 2021 Tüm Hakları Saklıdır
// </copyright>
// <author>İsmail KAŞAN</author>
// <date>1.07.2021</date>

using DomainExample;
using Microsoft.EntityFrameworkCore;

namespace DataExample
{
    /// <summary>
    /// Defines the <see cref="Entities" />.
    /// </summary>
    public class Entities : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Entities"/> class.
        /// </summary>
        /// <param name="options">The options<see cref="DbContextOptions{ExampleDbContext}"/>.</param>
        public Entities(DbContextOptions<ExampleDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the Product.
        /// </summary>
        public DbSet<Product> Product { get; set; }
    }
}
