using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PayCore.ProductCatalog.Application.Dto_Validator;
using PayCore.ProductCatalog.Application.Interfaces.Services;
using PayCore.ProductCatalog.Domain.Entities;
using System.Threading.Tasks;

namespace PayCore.ProductCatalog.WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ColorController : ControllerBase
    {
        private readonly IColorService colorService;

        public ColorController(IColorService categoryService, IColorService colorService)
        {
            this.colorService = colorService;
        }

        [HttpGet("getcolors")]
        public virtual async Task<IActionResult> GetAll()
        {
            //Fetching objects using service
            var result = await colorService.GetAll();
            return Ok(result);
        }

        [HttpGet("getcolorbyid")]
        public virtual async Task<IActionResult> GetById(int id)
        {
            //Fetching object using service
            var result = await colorService.GetById(id);
            return Ok(result);
        }

        [HttpPost("createcolor")]
        public virtual async Task<IActionResult> Create([FromBody] ColorUpsertDto dto)
        {
            await colorService.Insert(dto);
            return Ok();
        }

        [HttpPut("updatecolor")]
        public virtual async Task<IActionResult> Update(int id, [FromBody] ColorUpsertDto dto)
        {
            await colorService.Update(id, dto);
            return Ok();
        }

        [HttpDelete("deletecolor")]
        public virtual async Task<IActionResult> Delete(int id)
        {
            await colorService.Remove(id);
            return Ok();
        }
    }
}
