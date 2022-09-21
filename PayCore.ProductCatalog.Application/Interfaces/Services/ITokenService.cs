
using PayCore.ProductCatalog.Domain.Token;
using System.Threading.Tasks;

namespace PayCore.ProductCatalog.Application.Interfaces
{
    public interface ITokenService
    {
        Task<TokenResponse> GenerateToken(TokenRequest tokenRequest);
    }
}
