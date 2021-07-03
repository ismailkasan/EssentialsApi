// <copyright file="ProductController.cs" company="TC Sağlık Bakanlığı">
// Copyright(c) 2021 Tüm Hakları Saklıdır
// </copyright>
// <author>İsmail KAŞAN</author>
// <date>1.07.2021</date>

using AplicationExample;
using DomainExample;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace NetCoreExampleApi.Controllers
{
    /// <summary>
    /// Defines the <see cref="ProductController" />.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        /// <summary>
        /// Defines the _productService.
        /// </summary>
        private readonly IProductService _productService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductController"/> class.
        /// </summary>
        /// <param name="productService">The productService<see cref="IProductService"/>.</param>
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// The GetAll.
        /// </summary>
        /// <returns>The <see cref="ActionResult{IList{ProductOutputDto}}"/>.</returns>
        [HttpGet]
        [Route("GetAll")]
        [Authorize]
        public ActionResult<IList<ProductOutputDto>> GetAll()
        {
            var response = _productService.GetAllProducts();
            return Ok(response);
        }

        /// <summary>
        /// The GetProductFromSp.
        /// </summary>
        /// <param name="param">The param<see cref="string"/>.</param>
        /// <returns>The <see cref="ActionResult{IList{ProductOutputDto}}"/>.</returns>
        [HttpGet]
        [Route("GetProductFromSp")]
        public ActionResult<IList<ProductOutputDto>> GetProductFromSp([FromQuery] string param)
        {
            var response = _productService.GetProductFromSp(param);
            return Ok(response);
        }

        /// <summary>
        /// The Post.
        /// </summary>
        /// <param name="productInputDto">The productInputDto<see cref="ProductInputDto"/>.</param>
        /// <returns>The <see cref="ActionResult"/>.</returns>
        [HttpPost]
        public ActionResult Post([FromBody] ProductInputDto productInputDto)
        {
            _productService.AddProduct(productInputDto);
            return Ok();
        }
    }
}
