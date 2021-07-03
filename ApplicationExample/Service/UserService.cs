// <copyright file="UserService.cs" company="TC Sağlık Bakanlığı">
// Copyright(c) 2021 Tüm Hakları Saklıdır
// </copyright>
// <author>İsmail KAŞAN</author>
// <date>3.07.2021</date>

using DomainExample;
using System;

namespace ApplicationExample
{
    /// <summary>
    /// Defines the <see cref="UserService" />.
    /// </summary>
    public class UserService : IUserService
    {
        /// <summary>
        /// The Login.
        /// </summary>
        /// <param name="userInputDto">The userInputDto<see cref="UserInputDto"/>.</param>
        /// <returns>The <see cref="UserOutputDto"/>.</returns>
        public UserOutputDto Login(UserInputDto userInputDto)
        {
            return new UserOutputDto
            {
                UserName = "ismail",
                Password = "123456",
                Name = "ismail",
                Surname = "kaşan"
            };
        }
    }
}
