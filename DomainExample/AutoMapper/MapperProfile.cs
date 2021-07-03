using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainExample
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<ProductInputDto, Product>();
            CreateMap<Product, ProductOutputDto>();
            //.ForMember(d => d.Id, opt => opt.MapFrom(src => src.Id))
            //.ForMember(d => d.Name, operation => operation.MapFrom(source => source.Name))
            //.ForMember(d => d.Name, operation => operation.MapFrom(source => source.IsDeleted));
        }
    }
}
