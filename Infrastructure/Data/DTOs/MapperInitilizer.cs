using AutoMapper;
using Core.Entities;

namespace Infrastructure.Data.DTOs
{
    public class MapperInitilizer : Profile
    {
        public MapperInitilizer()
        {
            CreateMap<Product,ProductDTO>().ReverseMap();
        }
    }
}