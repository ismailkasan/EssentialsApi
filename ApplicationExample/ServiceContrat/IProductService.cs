// <copyright file="IProductService.cs" company="TC Sağlık Bakanlığı">
// Copyright(c) 2021 Tüm Hakları Saklıdır
// </copyright>
// <author>İsmail KAŞAN</author>
// <date>1.07.2021</date>

using CommonExample;
using DomainExample;
using System.Collections.Generic;

namespace AplicationExample
{
    /// <summary>
    /// Defines the <see cref="IProductService" />.
    /// </summary>
    public interface IProductService : IScopedLifetime
    {
        /// <summary>
        /// The GetAllProducts.
        /// </summary>
        /// <returns>The <see cref="IList{ProductOutputDto}"/>.</returns>
        IList<ProductOutputDto> GetAllProducts();

        /// <summary>
        /// The GetProductFromSp.
        /// </summary>
        /// <param name="param">The param<see cref="string"/>.</param>
        /// <returns>The <see cref="IList{ProductOutputDto}"/>.</returns>
        IList<ProductOutputDto> GetProductFromSp(string param);

        /// <summary>
        /// The AddProduct.
        /// </summary>
        /// <param name="productInputDto">The productInputDto<see cref="ProductInputDto"/>.</param>
        void AddProduct(ProductInputDto productInputDto);
    }
}
