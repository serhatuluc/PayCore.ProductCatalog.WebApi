using PayCore.ProductCatalog.Application.Dto_Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayCore.ProductCatalog.Application.Interfaces.Services
{
    public interface IColorService
    {
        Task<IEnumerable<ColorViewDto>> GetAll();
        Task<ColorViewDto> GetById(int id);
        Task Insert(ColorUpsertDto dto);
        Task Remove(int id);
        Task Update(int id, ColorUpsertDto dto);
    }
}
