using Allup.Application.ViewModels;
using Allup.Domain.Entities;
using AutoMapper;

namespace Allup.Application.Profiles;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        CreateMap<LanguageViewModel, Language>().ReverseMap();
        CreateMap<CurrencyViewModel, Currency>().ReverseMap();
        CreateMap<Category, CategoryViewModel>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.CategoryTranslations!.FirstOrDefault() == null ? "" : src.CategoryTranslations!.FirstOrDefault()!.Name)).ReverseMap();
        CreateMap<Product, ProductViewModel>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ProductTranslations!.FirstOrDefault() == null ? "" : src.ProductTranslations!.FirstOrDefault()!.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.ProductTranslations!.FirstOrDefault() == null ? "" : src.ProductTranslations!.FirstOrDefault()!.Description))
            .ReverseMap();
        CreateMap<Product, ProductCreateViewModel>().ReverseMap();
        CreateMap<CategoryTranslationViewModel, CategoryTranslation>().ReverseMap();
        CreateMap<ProductTranslationViewModel, ProductTranslation>().ReverseMap();
        CreateMap<ProductTranslationCreateViewModel, ProductTranslation>().ReverseMap();
        CreateMap<Wishlist, WishlistViewModel>().ReverseMap();
        CreateMap<Wishlist, WishlistCreateViewModel>().ReverseMap();
        CreateMap<ProductImage, ProductImageCreateViewModel>().ReverseMap();
        CreateMap<ProductImage, ProductImageViewModel>().ReverseMap();
    }
}
