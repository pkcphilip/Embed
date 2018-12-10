using AutoMapper;
using Embed.Core.Entities;
using Embed.Web.Core.Dtos;

namespace Embed.Web.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductBasicDto>()
                .ReverseMap();

            CreateMap<Product, ProductDto>()
                .ReverseMap();
        }
    }
}