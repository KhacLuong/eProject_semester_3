using AutoMapper;
using ShradhaBook_ClassLibrary.Entities;
using ShradhaBook_ClassLibrary.ViewModels;

namespace ShradhaBook_API.Helpers;

public class ApplicationMapper : Profile
{
    public ApplicationMapper()
    {
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<UserInfo, UserInfoDto>().ReverseMap();
        CreateMap<Address, AddressDto>().ReverseMap();
        CreateMap<Order, OrderDto>().ReverseMap();
        CreateMap<OrderItems, OrderItemsDto>().ReverseMap();

        CreateMap<Category, CategoryModelGet>().ForMember(item => item.Status, otp => otp.MapFrom(entity =>
            entity.Status == MyStatus.ACTIVE ? MyStatus.ACTIVE_RESULT : MyStatus.INACTIVE_RESULT));
        CreateMap<CategoryModelPost, Category>().ForMember(entity => entity.Status,
                otp => otp.MapFrom(item =>
                    item.Status.Trim().Equals(MyStatus.ACTIVE_RESULT) ? MyStatus.ACTIVE : MyStatus.INACTIVE))
            .ForMember(entity => entity.Code, otp => otp.MapFrom(item => item.Code.Trim().ToUpper()));
        CreateMap<Author, AuthorModelGet>();
        CreateMap<AuthorModelPost, Author>();

        CreateMap<Manufacturer, ManufacturerModelGet>();
        CreateMap<ManufacturerModelPost, Manufacturer>().ForMember(entity => entity.Code,
            otp => otp.MapFrom(item => item.Code.Trim().ToUpper()));

        CreateMap<Product, ProductModelGet>().ForMember(item => item.Status,
            otp => otp.MapFrom(entity =>
                entity.Status == MyStatus.ACTIVE ? MyStatus.ACTIVE_RESULT : MyStatus.INACTIVE_RESULT));

        CreateMap<ProductModelPost, Product>().ForMember(entity => entity.Status,
                otp => otp.MapFrom(item =>
                    item.Status.Trim().Equals(MyStatus.ACTIVE_RESULT) ? MyStatus.ACTIVE : MyStatus.INACTIVE))
            .ForMember(entity => entity.Code, otp => otp.MapFrom(item => item.Code.Trim().ToUpper()));

        CreateMap<Product, ProductModel>().ForMember(item => item.Status,
            otp => otp.MapFrom(entity =>
                entity.Status == MyStatus.ACTIVE ? MyStatus.ACTIVE_RESULT : MyStatus.INACTIVE_RESULT));


        CreateMap<Tag, TagModelGet>();
        CreateMap<TagModelPost, Tag>();


        CreateMap<ProductTag, ProductTagGet>();
        CreateMap<ProductTagPost, ProductTag>();

        CreateMap<Blog, BlogModelGet>().ForMember(item => item.Status,
            otp => otp.MapFrom(entity =>
                entity.Status == MyStatus.ACTIVE ? MyStatus.ACTIVE_RESULT : MyStatus.INACTIVE_RESULT));
        CreateMap<BlogModelPost, Blog>().ForMember(entity => entity.Status,
            otp => otp.MapFrom(item =>
                item.Status.Trim().Equals(MyStatus.ACTIVE_RESULT) ? MyStatus.ACTIVE : MyStatus.INACTIVE));

        CreateMap<ProductTag, ProductTagGet>();
        CreateMap<ProductTagPost, ProductTag>();

        CreateMap<BlogTag, BlogTagModelGet>();
        CreateMap<BlogTagModelPost, BlogTag>();

        CreateMap<WishList, BlogTagModelGet>();
        CreateMap<BlogTagModelPost, BlogTag>();

        CreateMap<WishList, WishListGet>();
        CreateMap<WishListPost, WishList>();

        CreateMap<WishListProduct, WishListProductGet>();
        CreateMap<WishListProductPost, WishListProduct>();

        CreateMap<Rate, RateModelGet>();
        CreateMap<RateModelPost, Rate>();

        CreateMap<Comment, CommentModelGet>();
        CreateMap<CommentModelPost, Comment>();
    }
}