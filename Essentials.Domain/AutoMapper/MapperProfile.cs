using AutoMapper;

namespace Essentials.Domain
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<ProductInputModel, Product>();
            CreateMap<Product, ProductOutputModel>();
            //.ForMember(d => d.Id, opt => opt.MapFrom(src => src.Id))
            //.ForMember(d => d.Name, operation => operation.MapFrom(source => source.Name))
            //.ForMember(d => d.Name, operation => operation.MapFrom(source => source.IsDeleted));
        }
    }
}
