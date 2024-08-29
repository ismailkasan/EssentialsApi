using Essentials.Aplication;
using Essentials.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Essentials.Api.Controllers
{

    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController(IProductService productService) : ControllerBase
    {       
        private readonly IProductService _productService = productService;

        [HttpGet]
        [Route("GetAll")]
        [Authorize]
        public ActionResult<IList<ProductOutputModel>> GetAll()
        {
            var response = _productService.GetAllProducts();
            return Ok(response);
        }

        [HttpGet]
        [Route("GetProductFromSp")]
        public ActionResult<IList<ProductOutputModel>> GetProductFromSp([FromQuery] string param)
        {
            var response = _productService.GetProductFromSp(param);
            return Ok(response);
        }

        [HttpPost]
        [Route("AddProduct")]
        public IList<string> Post([FromBody] ProductInputModel productInputModel)
        {
           return _productService.AddProduct(productInputModel);           
        }
    }
}
