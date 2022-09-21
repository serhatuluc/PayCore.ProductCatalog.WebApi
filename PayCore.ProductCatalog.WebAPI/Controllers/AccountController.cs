

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PayCore.ProductCatalog.Application.Dto_Validator.Account.Dto;
using PayCore.ProductCatalog.Application.Interfaces.Services;
using PayCore.ProductCatalog.Domain.Entities;
using System.Threading.Tasks;

namespace PayCore.ProductCatalog.WebAPI.Controllers
{
    [Authorize(Roles =Role.Admin)]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [HttpGet("getallaccounts")]
        public virtual async Task<IActionResult> GetAll()
        {
            //Fetching objects using service
            var result = await accountService.GetAll();
            return Ok(result);
        }

        [HttpGet("getaccountbyid")]
        public virtual async Task<IActionResult> GetById(int id)
        {
            //Fetching object using service
            var result = await accountService.GetById(id);
            return Ok(result);
        }

        [HttpPost("createaccount")]
        public virtual async Task<IActionResult> Create([FromBody] AccountUpsertDto dto)
        {
            await accountService.Insert(dto);
            return Ok();
        }

        [HttpPut("updateaccount")]
        public virtual async Task<IActionResult> Update(int id, [FromBody] AccountUpsertDto dto)
        {
            await accountService.Update(id, dto);
            return Ok();
        }

        [HttpDelete("deleteaccount")]
        public virtual async Task<IActionResult> Delete(int id)
        {
            await accountService.Remove(id);
            return Ok();
        }
    }
}
