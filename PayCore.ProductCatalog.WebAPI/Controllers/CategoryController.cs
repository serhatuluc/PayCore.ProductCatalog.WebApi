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
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet("getcategories")]
        public virtual async Task<IActionResult> GetAll()
        {
            //Fetching objects using service
            var result = await categoryService.GetAll();
            return Ok(result);
        }

        [HttpGet("getcategorybyid")]
        public virtual async Task<IActionResult> GetById(int id)
        {
            //Fetching object using service
            var result = await categoryService.GetById(id);
            return Ok(result);
        }

        [HttpPost("createcategory")]
        public virtual async Task<IActionResult> Create([FromBody] CategoryUpsertDto dto)
        {
            await categoryService.Insert(dto);
            return Ok();
        }

        [HttpPut("updatecategory")]
        public virtual async Task<IActionResult> Update(int id, [FromBody] CategoryUpsertDto dto)
        {
            await categoryService.Update(id, dto);
            return Ok();
        }

        [HttpDelete("deletecategory")]
        public virtual async Task<IActionResult> Delete(int id)
        {
            await categoryService.Remove(id);
            return Ok();
        }
    }
}
