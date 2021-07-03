// <copyright file="ProductRepository.cs" company="TC Sağlık Bakanlığı">
// Copyright(c) 2021 Tüm Hakları Saklıdır
// </copyright>
// <author>İsmail KAŞAN</author>
// <date>1.07.2021</date>

using DomainExample;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
namespace DataExample
{
    /// <summary>
    /// Defines the <see cref="ProductRepository" />.
    /// </summary>
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly ExampleDbContext _dbContext;
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductRepository"/> class.
        /// </summary>
        /// <param name="dbContext">The dbContext<see cref="ExampleDbContext"/>.</param>

        public ProductRepository(ExampleDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IList<ProductOutputDto> GetFromStoredProcedure(string param)
        {
            return _dbContext.Product
                //.FromSqlRaw<Product>("GetProducts @p0", param)
                .FromSqlRaw($"GetProducts {param}")
                .Select(a => new ProductOutputDto
                {
                    Id = a.Id,
                    IsDeleted = a.IsDeleted,
                    Name = a.Name

                }).ToList();
        }
    }
}
