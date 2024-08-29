using System.Collections.Generic;
using Essentials.Domain;

namespace Essentials.Data
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        IList<ProductOutputModel> GetFromStoredProcedure(string param);
    }
}
