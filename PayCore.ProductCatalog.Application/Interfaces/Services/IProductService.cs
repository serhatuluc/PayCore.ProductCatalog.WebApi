using PayCore.ProductCatalog.Application.Dto_Validator.Product.Dto;
using PayCore.ProductCatalog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PayCore.ProductCatalog.Application.Interfaces.Services
{
    public interface IProductService
    {
        //Fetches all products
        #nullable enable
        Task<IEnumerable<ProductViewDto>> GetAll(Expression<Func<Product, bool>>? expression = null);

        //Get product with id
        Task<ProductViewDto> GetById(int id);

        //Insert offerable Product 
        Task InsertProduct(int userId,ProductUpsertDto dto);

        //Remove product
        Task Remove(int productId,int userId);

        //Update product
        Task Update(int productId, int userId, ProductUpsertDto dto);

        //Buy without offering
        Task BuyProductWithoutOffer(int productId, int userId);

    }
}
