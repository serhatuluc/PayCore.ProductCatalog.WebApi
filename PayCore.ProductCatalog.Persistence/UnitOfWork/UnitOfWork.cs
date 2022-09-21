using NHibernate;
using PayCore.ProductCatalog.Application.Interfaces.Log;
using PayCore.ProductCatalog.Application.Interfaces.Repositories;
using PayCore.ProductCatalog.Application.Interfaces.UnitOfWork;
using PayCore.ProductCatalog.Persistence.Repositories;

namespace PayCore.ProductCatalog.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ILoggerManager logger;
        private readonly ISession session;
        public IBrandRepository _Brand;
        public ICategoryRepository _Category;
        public IOfferRepository _Offer;
        public IColorRepository _Color;
        public IProductRepository _Product;
        public IAccountRepository _Account;

        public UnitOfWork(ISession session, ILoggerManager logger, ICategoryRepository _Category, IBrandRepository _Brand, IColorRepository _Color, IOfferRepository _Offer, IProductRepository _Product,IAccountRepository _Account)
        {
            this.session = session;
            this.logger = logger;
            this._Brand = _Brand;
            this._Category = _Category;
            this._Color = _Color;
            this._Offer = _Offer;
            this._Product = _Product;
            this._Account = _Account;

        }
        public IBrandRepository Brand => _Brand ??= new BrandRepository(session,logger);
        public ICategoryRepository Category => _Category ??= new CategoryRepository(session,logger);
        public IColorRepository Color => _Color ??= new ColorRepository(session,logger);
        public IOfferRepository Offer => _Offer ??= new OfferRepository(session,logger);
        public IProductRepository Product => _Product ??= new ProductRepository(session,logger);
        public IAccountRepository Account => _Account ??= new AccountRepository(session,logger);

    }
}
