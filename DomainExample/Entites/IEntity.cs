// <copyright file="IEntity.cs" company="TC Sağlık Bakanlığı">
// Copyright(c) 2021 Tüm Hakları Saklıdır
// </copyright>
// <author>İsmail KAŞAN</author>
// <date>1.07.2021</date>

namespace DomainExample
{
    /// <summary>
    /// Defines the <see cref="IEntity" />.
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        long Id { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether IsDeleted.
        /// </summary>
        bool IsDeleted { get; set; }
    }
}
