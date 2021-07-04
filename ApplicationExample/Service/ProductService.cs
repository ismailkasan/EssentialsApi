// <copyright file="ProductService.cs" company="TC Sağlık Bakanlığı">
// Copyright(c) 2021 Tüm Hakları Saklıdır
// </copyright>
// <author>İsmail KAŞAN</author>
// <date>1.07.2021</date>

using AutoMapper;
using DataExample;
using DomainExample;
using FluentValidation;
using System.Collections.Generic;
using System.Linq;

namespace AplicationExample
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;

        private readonly IProductRepository _productRepository;

        private readonly IValidator<Product> _productValidator;

        public ProductService(IProductRepository productRepository, IMapper mapper, IValidator<Product> productValidator)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _productValidator = productValidator;
        }

        public IList<string> AddProduct(ProductInputDto productInputDto)
        {
            Product product = _mapper.Map<Product>(productInputDto);
            var validateResult = _productValidator.Validate(product);
            List<string> messages = new List<string>();
            if (validateResult.IsValid)
            {
                _productRepository.Save(product);
                messages.Add("Başarılı");
                return messages;
            }
            else
            {
                foreach (var item in validateResult.Errors)
                {
                    messages.Add(item.ErrorMessage);
                }
                return messages;
            }
        }

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
