// <copyright file="Product.cs" company="TC Sağlık Bakanlığı">
// Copyright(c) 2021 Tüm Hakları Saklıdır
// </copyright>
// <author>İsmail KAŞAN</author>
// <date>30.06.2021</date>

namespace DomainExample
{
    /// <summary>
    /// Defines the <see cref="Product" />.
    /// </summary>

    public class Product : BaseEntitiy
    {
        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        public string Name { get; set; }
    }
}
