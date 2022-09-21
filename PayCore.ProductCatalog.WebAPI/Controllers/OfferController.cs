using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PayCore.ProductCatalog.Application.Dto_Validator;
using PayCore.ProductCatalog.Application.Interfaces.Services;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PayCore.ProductCatalog.WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OfferController : ControllerBase
    {
        private readonly IOfferService offerService;

        public OfferController(IOfferService offerService)
        {
            this.offerService = offerService;
        }

        [HttpGet("getoffersofuser")]
        public virtual async Task<IActionResult> GetAllOffersOfUser()
        {
            var accountId = int.Parse((User.Identity as ClaimsIdentity).FindFirst("AccountId").Value);
            var result = await offerService.GetOffersofUser(accountId);
            return Ok(result);
        }

        [HttpGet("getofferstouser")]
        public virtual async Task<IActionResult> GetAllOfferstoUser()
        {
            var accountId = int.Parse((User.Identity as ClaimsIdentity).FindFirst("AccountId").Value);
            var result = await offerService.GetOffersToUser(accountId);
            return Ok(result);
        }


        [HttpPost("offeronproduct")]
        public virtual async Task<IActionResult> Create([FromBody] OfferUpsertDto dto)
        {
            var accountId = int.Parse((User.Identity as ClaimsIdentity).FindFirst("AccountId").Value);
            await offerService.OfferOnProduct(accountId,dto);
            return Ok();
        }

        [HttpPut("updateoffer")]
        public virtual async Task<IActionResult> Update(int offerId, [FromBody] OfferUpsertDto dto)
        {
            var accountId = int.Parse((User.Identity as ClaimsIdentity).FindFirst("AccountId").Value);
            await offerService.UpdateOffer(accountId,offerId, dto);
            return Ok();
        }

        [HttpDelete("withdrawoffer")]
        public virtual async Task<IActionResult> Delete(int offerId)
        {
            var accountId = int.Parse((User.Identity as ClaimsIdentity).FindFirst("AccountId").Value);
            await offerService.WithDrawOffer(accountId,offerId);
            return Ok();
        }

        [HttpPut("approveoffer")]
        public virtual async Task<IActionResult> ApproveOffer(int offerId)
        {
            var accountId = int.Parse((User.Identity as ClaimsIdentity).FindFirst("AccountId").Value);
            await offerService.ApproveOffer(offerId, accountId);
            return Ok();
        }

        [HttpPut("disapproveoffer")]
        public virtual async Task<IActionResult> DisapproveOffer(int offerId)
        {
            var accountId = int.Parse((User.Identity as ClaimsIdentity).FindFirst("AccountId").Value);
            await offerService.DisapproveOffer(offerId, accountId);
            return Ok();
        }

    }
}
