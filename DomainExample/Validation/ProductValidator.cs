// <copyright file="ProductValidator.cs" company="TC Sağlık Bakanlığı">
// Copyright(c) 2021 Tüm Hakları Saklıdır
// </copyright>
// <author>İsmail KAŞAN</author>
// <date>4.07.2021</date>

using FluentValidation;

namespace DomainExample
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Lütfen ürün adını boş geçmeyiniz.");
            RuleFor(c => c.IsDeleted).Must(d=>d==true).WithMessage("Lütfen IsDeletd true gönderiniz.");
            RuleFor(c => c.Id).Must(d => d == 0).WithMessage("Lütfen Id değerini göndermeyiniz.");
        }
    }
}
