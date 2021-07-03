// <copyright file="ProductService.cs" company="TC Sağlık Bakanlığı">
// Copyright(c) 2021 Tüm Hakları Saklıdır
// </copyright>
// <author>İsmail KAŞAN</author>
// <date>1.07.2021</date>

using AutoMapper;
using DataExample;
using DomainExample;
using System.Collections.Generic;
using System.Linq;

namespace AplicationExample
{
    /// <summary>
    /// Defines the <see cref="ProductService" />.
    /// </summary>
    public class ProductService : IProductService
    {
        /// <summary>
        /// Defines the _productRepository.
        /// </summary>
        private readonly IProductRepository _productRepository;

        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductService"/> class.
        /// </summary>
        /// <param name="productRepository">The productRepository<see cref="IProductRepository"/>.</param>
        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// The AddProduct.
        /// </summary>
        /// <param name="productInputDto">The productInputDto<see cref="ProductInputDto"/>.</param>
        public void AddProduct(ProductInputDto productInputDto)
        {
            Product product = _mapper.Map<Product>(productInputDto);
            _productRepository.Save(product);
        }

        /// <summary>
        /// The GetAllProducts.
        /// </summary>
        /// <returns>The <see cref="IList{ProductOutputDto}"/>.</returns>
        public IList<ProductOutputDto> GetAllProducts()
        {
            var sources = _productRepository.All().ToList();
           
            IList<ProductOutputDto> result = _mapper.Map<IList<Product>, IList<ProductOutputDto>>(sources);
            return result;
        }

        public IList<ProductOutputDto> GetProductFromSp(string param)
        {
            return _productRepository.GetFromStoredProcedure(param);
        }
    }
}
