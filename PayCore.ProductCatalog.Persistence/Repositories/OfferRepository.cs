using NHibernate;
using NHibernate.Linq;
using PayCore.ProductCatalog.Application.Interfaces.Log;
using PayCore.ProductCatalog.Application.Interfaces.Repositories;
using PayCore.ProductCatalog.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayCore.ProductCatalog.Persistence.Repositories
{
    public class OfferRepository : GenericRepository<Offer>, IOfferRepository
    {
        private readonly ILoggerManager Logger;
        private readonly ISession session;
        private ITransaction transaction;

        public OfferRepository(ISession session, ILoggerManager Logger) : base(session, Logger)
        {
            this.session = session;
            this.Logger = Logger;
        }

        public async Task<IList<Offer>> GetOfferOfUser(int userId)
        {
           return await session.Query<Offer>().Where(x=>x.Customer.Id == userId).ToListAsync();
        }

        public async Task<IList<Offer>> GetOfferstoUser(int userID)
        {
            var result = await session.Query<Offer>().Where(x=>x.Product.Owner.Id == userID).ToListAsync();
            return result;
        }
    }
}
