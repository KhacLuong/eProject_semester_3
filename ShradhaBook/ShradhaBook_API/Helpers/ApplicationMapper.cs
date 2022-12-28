using AutoMapper;
using ShradhaBook_API.ViewModels;

namespace ShradhaBook_API.Helpers
{
    public class ApplicationMapper : Profile
    {

        public ApplicationMapper()
        {

            CreateMap<Category, CategoryModelGet>().ForMember(item => item.Status, otp => otp.MapFrom(entity => entity.Status == MyStatus.ACTIVE ? MyStatus.ACTIVE_RESULT : MyStatus.INACTIVE_RESULT));
            CreateMap<CategoryModelPost, Category>().ForMember(entity => entity.Status, otp => otp.MapFrom(item => item.Status.Trim().Equals(MyStatus.ACTIVE_RESULT) ? MyStatus.ACTIVE : MyStatus.INACTIVE))
               .ForMember(entity => entity.Code, otp => otp.MapFrom(item => item.Code.Trim().ToUpper()));
            CreateMap<Author, AuthorModelGet>();
            CreateMap<AuthorModelPost, Author>();

            CreateMap<Manufacturer, ManufacturerModelGet>();
            CreateMap<ManufacturerModelPost, Manufacturer>().ForMember(entity => entity.Code, otp => otp.MapFrom(item => item.Code.Trim().ToUpper()));

            CreateMap<Product, ProductModelGet>().ForMember(item => item.Status, otp => otp.MapFrom(entity => entity.Status == MyStatus.ACTIVE ? MyStatus.ACTIVE_RESULT : MyStatus.INACTIVE_RESULT))
               ;
               
            CreateMap<ProductModelPost, Product>().ForMember(entity => entity.Status, otp => otp.MapFrom(item => item.Status.Trim().Equals(MyStatus.ACTIVE_RESULT) ? MyStatus.ACTIVE : MyStatus.INACTIVE))
               .ForMember(entity => entity.Code, otp => otp.MapFrom(item => item.Code.Trim().ToUpper()));

            CreateMap<Tag, TagModelGet>();
            CreateMap<TagModelPost, Tag>();


            CreateMap<ProductTag, ProductTagGet>();
            CreateMap<ProductTagPost, ProductTag>();

            CreateMap<Blog, BlogModelGet>().ForMember(item => item.Status, otp => otp.MapFrom(entity => entity.Status == MyStatus.ACTIVE ? MyStatus.ACTIVE_RESULT : MyStatus.INACTIVE_RESULT));
            CreateMap<BlogModelPost, Blog>().ForMember(entity => entity.Status, otp => otp.MapFrom(item => item.Status.Trim().Equals(MyStatus.ACTIVE_RESULT) ? MyStatus.ACTIVE : MyStatus.INACTIVE));



        }
    }
}
