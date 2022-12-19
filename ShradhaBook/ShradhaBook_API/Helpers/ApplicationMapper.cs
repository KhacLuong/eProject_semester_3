using AutoMapper;
using ShradhaBook_API.ViewModels;

namespace ShradhaBook_API.Helpers
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<Category, CategoryModel>().ReverseMap();
            CreateMap<Manufacturer, ManufacturerModel>().ReverseMap();
            CreateMap<Product, ProductModel>().ReverseMap();
            CreateMap<Combo, ComboModel>().ReverseMap();
            CreateMap<Tag, TagModel>().ReverseMap();
            CreateMap<ComboTag, ComboTagModel>().ReverseMap();
            CreateMap<ProductTag, ProductTagModel>().ReverseMap();
            CreateMap<ComboProduct, ComboProductModel>().ReverseMap();



        }
    }
}
