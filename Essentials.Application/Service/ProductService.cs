using AutoMapper;
using Essentials.Data;
using Essentials.Domain;
using FluentValidation;
using System.Collections.Generic;
using System.Linq;

namespace Essentials.Aplication
{
    public class ProductService(
        IProductRepository productRepository, 
        IMapper mapper,
        IValidator<Product> productValidator
        ) : IProductService
    {
        private readonly IMapper _mapper = mapper;
        private readonly IProductRepository _productRepository = productRepository;
        private readonly IValidator<Product> _productValidator = productValidator;

        public IList<string> AddProduct(ProductInputModel productInputModel)
        {
            Product product = _mapper.Map<Product>(productInputModel);
            var validateResult = _productValidator.Validate(product);

            List<string> messages = [];
            
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

        public IList<ProductOutputModel> GetAllProducts()
        {
            var sources = _productRepository.All().ToList();

            IList<ProductOutputModel> result = _mapper.Map<IList<Product>, IList<ProductOutputModel>>(sources);
            return result;
        }

        public IList<ProductOutputModel> GetProductFromSp(string param)
        {
            return _productRepository.GetFromStoredProcedure(param);
        }
    }
}
