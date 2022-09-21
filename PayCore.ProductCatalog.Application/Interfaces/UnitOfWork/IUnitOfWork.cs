using PayCore.ProductCatalog.Application.Interfaces.Repositories;


namespace PayCore.ProductCatalog.Application.Interfaces.UnitOfWork
{
    public interface IUnitOfWork
    {
        IBrandRepository Brand { get; }
        ICategoryRepository Category { get; }
        IColorRepository Color { get; }
        IProductRepository Product { get; }
        IOfferRepository Offer { get; }
        IAccountRepository Account { get; }
    }
}
