using PayCore.ProductCatalog.Application.Dto_Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayCore.ProductCatalog.Application.Interfaces.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryViewDto>> GetAll();
        Task<CategoryViewDto> GetById(int id);
        Task Insert(CategoryUpsertDto dto);
        Task Remove(int id);
        Task Update(int id, CategoryUpsertDto dto);
    }
}
