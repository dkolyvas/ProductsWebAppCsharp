using AutoMapper;
using ProductsDBApp.DTO;
using ProductsDBApp.Model;

namespace ProductsDBApp.Config
{
    public class MapperProduct: Profile
    {
        public MapperProduct() 
        {
            CreateMap<ProductInsertDTO, Product>().ReverseMap();
            CreateMap<ProductUpdateDTO, Product>().ReverseMap();
            CreateMap<ProductShowDTO, Product>().ReverseMap();
        }
    }
}
