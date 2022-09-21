using Hangfire;
using Microsoft.AspNetCore.Mvc;
using PayCore.ProductCatalog.Application.Interfaces.Mail;
using PayCore.ProductCatalog.Domain.Mail;
using System;
using System.Threading.Tasks;

namespace PayCore.ProductCatalog.WebAPI.Controllers
{
    [ApiController]
    [Route("api/Mail")]
    public class MailController : ControllerBase
    {
        private readonly IEmailService mailService;
        public MailController(IEmailService mailService)
        {
            this.mailService = mailService;
        }
        [HttpPost("send")]
        public async Task<IActionResult> SendMail([FromBody] MailRequest request)
        {
            var jobId = BackgroundJob.Schedule(() => mailService.SendEmailAsync(request), TimeSpan.Zero);
            return Ok(jobId);
        }

    }
}
