using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PayCore.ProductCatalog.Application.Dto_Validator;
using PayCore.ProductCatalog.Application.Interfaces.Mail;
using PayCore.ProductCatalog.Application.Interfaces.Services;
using PayCore.ProductCatalog.Domain.Mail;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PayCore.ProductCatalog.WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OfferController : ControllerBase
    {
        private readonly IEmailService _emailService;
        private readonly IOfferService offerService;

        public OfferController(IOfferService offerService, IEmailService emailService)
        {
            this._emailService = emailService;
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

        [AutomaticRetry(Attempts = 5)]
        [HttpPost("offeronproduct")]
        public virtual async Task<IActionResult> Create([FromBody] OfferUpsertDto dto)
        {
            var accountId = int.Parse((User.Identity as ClaimsIdentity).FindFirst("AccountId").Value);
            await offerService.OfferOnProduct(accountId,dto);

            //Sends mail to user after offer is taken
            var email = (User.Identity as ClaimsIdentity).FindFirst("Email").Value;
            BackgroundJob.Schedule(() => _emailService.SendEmailAsync(new MailRequest { ToEmail = email, From = email, Subject = "New Offer", Body = "Your offer is registered." }), TimeSpan.Zero);

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

        [AutomaticRetry(Attempts = 5)]
        [HttpPut("approveoffer")]
        public virtual async Task<IActionResult> ApproveOffer(int offerId)
        {
            var accountId = int.Parse((User.Identity as ClaimsIdentity).FindFirst("AccountId").Value);
            await offerService.ApproveOffer(offerId, accountId);

            //After offer is approved. Mail will be sent.
            //Sends mail to user after offer is taken
            var email = (User.Identity as ClaimsIdentity).FindFirst("Email").Value;
            BackgroundJob.Schedule(() => _emailService.SendEmailAsync(new MailRequest { ToEmail = email, From = email, Subject = "Approved Offer", Body = "Your approved offer." }), TimeSpan.Zero);
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
