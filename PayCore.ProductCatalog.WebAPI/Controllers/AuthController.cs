using Hangfire;
using Microsoft.AspNetCore.Mvc;
using PayCore.ProductCatalog.Application.Dto_Validator.Account.Dto;
using PayCore.ProductCatalog.Application.Interfaces;
using PayCore.ProductCatalog.Application.Interfaces.Mail;
using PayCore.ProductCatalog.Application.Interfaces.Services;
using PayCore.ProductCatalog.Domain.Mail;
using PayCore.ProductCatalog.Domain.Token;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PayCore.ProductCatalog.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAccountService accountService;
        private readonly ITokenService tokenService;
        private readonly IEmailService _emailService;

       
        public AuthController(IAccountService accountService, ITokenService tokenService, IEmailService emailService)
        {
            this.accountService = accountService;
            this.tokenService = tokenService;
            this._emailService = emailService;
        }

        
        [HttpPost("Login")]
        public async Task<TokenResponse> Login([FromBody] TokenRequest request)
        {
            var response = await tokenService.GenerateToken(request);
            return response;
        }

        [AutomaticRetry(Attempts = 5)]
        [HttpPost("register")]
        public virtual async Task<IActionResult> Create([FromBody] AccountUpsertDto dto)
        {
            await accountService.Insert(dto);
            //After registration. Mail is sent. 
            BackgroundJob.Schedule(() => _emailService.SendEmailAsync(new MailRequest { ToEmail = dto.Email, From = dto.Email, Subject = "Welcome", Body = "Hope. You enjoy your stay." }), TimeSpan.Zero);
            return Ok();
        }

        
    }
}
