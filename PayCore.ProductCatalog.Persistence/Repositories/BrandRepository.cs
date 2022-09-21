using NHibernate;
using PayCore.ProductCatalog.Application.Interfaces.Log;
using PayCore.ProductCatalog.Application.Interfaces.Repositories;
using PayCore.ProductCatalog.Domain.Entities;


namespace PayCore.ProductCatalog.Persistence.Repositories
{
    public class BrandRepository:GenericRepository<Brand>,IBrandRepository
    {
        private readonly ILoggerManager Logger;
        private readonly ISession session;
        private ITransaction transaction;

        public BrandRepository(ISession session, ILoggerManager Logger) : base(session,Logger)
        {
            this.session = session;
            this.Logger = Logger;
        }
    }
}
