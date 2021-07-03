// <copyright file="IProductRepository.cs" company="TC Sağlık Bakanlığı">
// Copyright(c) 2021 Tüm Hakları Saklıdır
// </copyright>
// <author>İsmail KAŞAN</author>
// <date>1.07.2021</date>

using CommonExample;
using DomainExample;
using System.Collections.Generic;

namespace DataExample
{
    /// <summary>
    /// Defines the <see cref="IProductRepository" />.
    /// </summary>
    public interface IProductRepository : IGenericRepository<Product>, IScopedLifetime
    {
        IList<ProductOutputDto> GetFromStoredProcedure(string param);
    }
}
