using PayCore.ProductCatalog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayCore.ProductCatalog.Application.Interfaces.Repositories
{
    public interface IOfferRepository:IGenericRepository<Offer>
    {
        Task<IList<Offer>> GetOfferOfUser(int userId);
        Task<IList<Offer>> GetOfferstoUser(int userId);
    }
}
