using AutoMapper;
using PayCore.ProducCatalog.Application.Dto_Validator;
using PayCore.ProductCatalog.Application.Dto_Validator;
using PayCore.ProductCatalog.Application.Dto_Validator.Account.Dto;
using PayCore.ProductCatalog.Application.Dto_Validator.Product.Dto;
using PayCore.ProductCatalog.Domain.Entities;

namespace PayCore.ProductCatalog.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Brand
            CreateMap<Brand, BrandViewDto>().ReverseMap();
            CreateMap<Brand, BrandUpsertDto>().ReverseMap();

            //Category
            CreateMap<Category, CategoryViewDto>().ReverseMap();
            CreateMap<Category, CategoryUpsertDto>().ReverseMap();

            //Color
            CreateMap<Color, ColorViewDto>().ReverseMap();
            CreateMap<Color, ColorUpsertDto>().ReverseMap();

            //Product
            CreateMap<Product, ProductViewDto>().ReverseMap();
            CreateMap<Product, ProductUpsertDto>().ReverseMap();

            //Offer
            CreateMap<Offer, OfferViewDto>().ReverseMap();
            CreateMap<Offer, OfferUpsertDto>().ReverseMap();

            //Account
            CreateMap<Account, AccountViewDto>().ReverseMap();
            CreateMap<Account, AccountUpsertDto>().ReverseMap();
        }
    }
}
