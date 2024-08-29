using Essentials.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Essentials.Data
{
    public class ProductRepository(EssentialsDbContext dbContext) : GenericRepository<Product>(dbContext), IProductRepository
    {
        private readonly EssentialsDbContext _dbContext = dbContext;

        public IList<ProductOutputModel> GetFromStoredProcedure(string param)
        {
            return _dbContext.Product
                //.FromSqlRaw<Product>("GetProducts @p0", param)
                .FromSqlRaw($"GetProducts {param}")
                .Select(a => new ProductOutputModel(a.Id,a.Name,a.IsDeleted)).ToList();
        }
    }
}
