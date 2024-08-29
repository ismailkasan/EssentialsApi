using System.Collections.Generic;
using Essentials.Domain;

namespace Essentials.Aplication
{   
    public interface IProductService
    {     
        IList<ProductOutputModel> GetAllProducts();
        IList<ProductOutputModel> GetProductFromSp(string param);  
        IList<string> AddProduct(ProductInputModel productInputModel);
    }
}
