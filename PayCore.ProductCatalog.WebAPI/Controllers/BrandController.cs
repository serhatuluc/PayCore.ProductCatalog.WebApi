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
    public class BrandController:ControllerBase
    {
        private readonly IBrandService brandService;

        public BrandController(IBrandService brandService)
        {
            this.brandService = brandService;
        }

        [HttpGet("getbrands")]
        public virtual async Task<IActionResult> GetAll()
        {
            //Fetching objects using service
            var result = await brandService.GetAll();
            return Ok(result);
        }

        [HttpGet("getbrandbyid")]
        public virtual async Task<IActionResult> GetById(int id)
        {
            //Fetching object using service
            var result = await brandService.GetById(id);
            return Ok(result);
        }

        [HttpPost("createbrand")]
        public virtual async Task<IActionResult> Create([FromBody] BrandUpsertDto dto)
        {
            await brandService.Insert(dto);
            return Ok();
        }

        [HttpPut("updatebrand")]
        public virtual async Task<IActionResult> Update(int id,[FromBody] BrandUpsertDto dto)
        {
            await brandService.Update(id,dto);
            return Ok();
        }

        [HttpDelete("deletebrand")]
        public virtual async Task<IActionResult> Delete(int id)
        {
            await brandService.Remove(id);
            return Ok();
        }
    }
}
