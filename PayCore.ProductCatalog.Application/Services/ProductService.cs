using AutoMapper;
using PayCore.ProductCatalog.Application.Dto_Validator.Product.Dto;
using PayCore.ProductCatalog.Application.Interfaces.Services;
using PayCore.ProductCatalog.Application.Interfaces.UnitOfWork;
using PayCore.ProductCatalog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PayCore.ProductCatalog.Application.Services
{
    public class ProductService : IProductService
    {
        protected readonly IMapper _mapper;
        protected readonly IUnitOfWork _unitOfWork;


        public ProductService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this._mapper = mapper;
            this._unitOfWork = unitOfWork;

        }

        //GetAll
        public async Task<IEnumerable<ProductViewDto>> GetAll(Expression<Func<Product, bool>> expression = null)
        {
            #nullable enable
            var listProduct = await _unitOfWork.Product.GetAll(expression);

            var result = new List<ProductViewDto>();


            using (var sequenceEnum = listProduct.GetEnumerator())
            {
                while (sequenceEnum.MoveNext())
                {
                    //Mapping to view model 
                    //Automapper is not preferred to be used here. Since this kind of mapping needs to be more distinct
                    var productView = new ProductViewDto()
                    {
                        Id = sequenceEnum.Current.Id,
                        ProductName = sequenceEnum.Current.ProductName,
                        Description = sequenceEnum.Current.Description,
                        CategoryName = sequenceEnum.Current.Category.CategoryName,
                        BrandName = sequenceEnum.Current.Brand.BrandName,
                        ColorName = sequenceEnum.Current.Color.ColorName,
                        Price = sequenceEnum.Current.Price,
                        IsSold = sequenceEnum.Current.IsSold,
                        IsOfferable = sequenceEnum.Current.IsOfferable,
                        Status = sequenceEnum.Current.Status,
                        OwnerId = sequenceEnum.Current.Owner.Id,
                        OwnerName = sequenceEnum.Current.Owner.Name,
                       
                    };
                    result.Add(productView);
                }
            }
            return result;
        }

        //GetById
        public async Task<ProductViewDto> GetById(int id)
        {
            var entity = await _unitOfWork.Product.GetById(id);

            if (entity is null)
            {
                throw new NotFoundException(nameof(Product), id);
            }

            var result = new ProductViewDto()
            {
                Id = entity.Id,
                ProductName = entity.ProductName,
                Description = entity.Description,
                CategoryName = entity.Category.CategoryName,
                BrandName = entity.Brand.BrandName,
                ColorName = entity.Color.ColorName,
                Price = entity.Price,
                IsSold = entity.IsSold,
                IsOfferable = entity.IsOfferable,
                Status = entity.Status,
                OwnerId = entity.Owner.Id,
                OwnerName = entity.Owner.Name,
            };
            return result;
        }

        //Insert
        public async Task InsertProduct(int UserId,ProductUpsertDto dto)
        {
            var tempEntity = _mapper.Map<ProductUpsertDto, Product>(dto);

            //Category id which is taken from dto is used to assign category to product 
            tempEntity.Category = await _unitOfWork.Category.GetById(dto.CategoryId);
            if (tempEntity.Category is null) 
            {
                throw new NotFoundException(nameof(Category), dto.CategoryId); 
            }

            //Brand id which is taken from dto is used to assign brand to product 
            tempEntity.Brand = await _unitOfWork.Brand.GetById(dto.BrandId);
            if (tempEntity.Brand is null)
            {
                throw new NotFoundException(nameof(Category), dto.BrandId);
            }

            //Color id which is taken from dto is used to assign color to product 
            tempEntity.Color = await _unitOfWork.Color.GetById(dto.ColorId);
            if (tempEntity.Color is null)
            {
                throw new NotFoundException(nameof(Category), dto.ColorId);
            }

            //Account id taken from jwt token is used to assignt the product to account
            tempEntity.Owner = await _unitOfWork.Account.GetById(UserId);
            await _unitOfWork.Product.Create(tempEntity);
        }

        //Remove
        public async Task Remove(int productId, int userId)
        {
            var entity = await _unitOfWork.Product.GetById(productId);

            if (entity is null)
            {
                throw new NotFoundException(nameof(Product), productId);
            }

            if(userId != entity.Owner.Id)
            {
                throw new BadRequestException("Not allowed");
            }

            await _unitOfWork.Product.Delete(entity);
        }

        //Update
        public async Task Update(int productId, int userId,ProductUpsertDto dto)
        {
            var tempentity = await _unitOfWork.Offer.GetById(productId);
            if (tempentity is null)
            {
                throw new NotFoundException(nameof(Product), productId);
            }

            var tempEntity = _mapper.Map<ProductUpsertDto, Product>(dto);

            //Category id which is taken from dto is used to assign category to product 
            tempEntity.Category = await _unitOfWork.Category.GetById(dto.CategoryId);
            if (tempEntity.Category is null)
            {
                throw new NotFoundException(nameof(Category), dto.CategoryId);
            }

            //Brand id which is taken from dto is used to assign brand to product 
            tempEntity.Brand = await _unitOfWork.Brand.GetById(dto.BrandId);
            if (tempEntity.Brand is null)
            {
                throw new NotFoundException(nameof(Category), dto.BrandId);
            }

            //Color id which is taken from dto is used to assign color to product 
            tempEntity.Color = await _unitOfWork.Color.GetById(dto.ColorId);
            if (tempEntity.Color is null)
            {
                throw new NotFoundException(nameof(Category), dto.ColorId);
            }

            //Account id taken from jwt token is used to assignt the product to account
            tempEntity.Owner = await _unitOfWork.Account.GetById(userId);
            await _unitOfWork.Offer.Update(tempentity);
        }


        public async Task BuyProductWithoutOffer(int productId,int userId )
        {
            var entity = await _unitOfWork.Product.GetById(productId);

            //if product doesnt exist
            if(entity is null)
            {
                throw new NotFoundException(nameof(Product), productId);
            }

            //if product belongs to user
            if (entity.Owner.Id == userId)
            {
                throw new BadRequestException("Product belongs to user");
            }

            //If product is sold
            if(entity.IsSold == true)
            {
                throw new BadRequestException("Product is sold");
            }

            entity.IsSold = true;
            entity.Status = false;
            entity.IsOfferable = false;
            await _unitOfWork.Product.Update(entity);

            //Since product is sold all other offers on this product  will be inactive. So status will be false
            var allOffers = await _unitOfWork.Offer.GetAll();
            var offersofProduct = allOffers.Where(x => x.Product.Id == productId);
            foreach(var offer in offersofProduct)
            {
                offer.Status = false;
            }
        }
    }
}
