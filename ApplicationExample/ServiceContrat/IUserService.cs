// <copyright file="IUserService.cs" company="TC Sağlık Bakanlığı">
// Copyright(c) 2021 Tüm Hakları Saklıdır
// </copyright>
// <author>İsmail KAŞAN</author>
// <date>3.07.2021</date>

using CommonExample;
using DomainExample;

namespace ApplicationExample
{
    /// <summary>
    /// Defines the <see cref="IUserService" />.
    /// </summary>
    public interface IUserService : IScopedLifetime
    {
        /// <summary>
        /// The Login.
        /// </summary>
        /// <param name="userInputDto">The userInputDto<see cref="UserInputDto"/>.</param>
        /// <returns>The <see cref="UserOutputDto"/>.</returns>
        UserOutputDto Login(UserInputDto userInputDto);
    }
}
