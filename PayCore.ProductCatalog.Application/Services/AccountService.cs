using AutoMapper;
using PayCore.ProductCatalog.Application.Dto_Validator.Account.Dto;
using PayCore.ProductCatalog.Application.Interfaces.Services;
using PayCore.ProductCatalog.Application.Interfaces.UnitOfWork;
using PayCore.ProductCatalog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayCore.ProductCatalog.Application.Services
{
    public class AccountService : IAccountService
    {
        protected readonly IMapper mapper;
        protected readonly IUnitOfWork unitOfWork;


        public AccountService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;

        }

        //GetAll
        public async Task<IEnumerable<AccountViewDto>> GetAll()
        {
            var tempEntity = await unitOfWork.Account.GetAll();
            var result = mapper.Map<IEnumerable<Account>, IEnumerable<AccountViewDto>>(tempEntity);
            return result;
        }

        //GetById
        public async Task<AccountViewDto> GetById(int id)
        {
            var entity = await unitOfWork.Account.GetById(id);

            if (entity is null)
            {
                throw new NotFoundException(nameof(Account), id);
            }

            var result = mapper.Map<Account, AccountViewDto>(entity);
            return result;
        }

        //Insert
        public async Task Insert(AccountUpsertDto dto)
        {
            var accounts = await unitOfWork.Account.GetAll(x => x.UserName == dto.UserName);
            if(accounts.Count() != 0)
            {
                throw new BadRequestException("This username is used by another user");
            }
            var tempEntity = mapper.Map<AccountUpsertDto, Account>(dto);
            tempEntity.Password = tempEntity.Password.GetMd5Hash();
            await unitOfWork.Account.Create(tempEntity);
        }

        //Remove
        public async Task Remove(int id)
        {
            var entity = await unitOfWork.Account.GetById(id);

            if (entity is null)
            {
                throw new NotFoundException(nameof(Account), id);
            }

            //IsDeleted field of brand is updated to delete. 
            //Assuming product might have used this brand id. The brand is not delted from database 
            await unitOfWork.Account.Delete(entity);
        }

        //Update
        public async Task Update(int id, AccountUpsertDto dto)
        {
            var tempentity = await unitOfWork.Account.GetById(id);
            if (tempentity is null)
            {
                throw new NotFoundException(nameof(Account), id);
            }
            tempentity.Email = dto.Email;
            tempentity.Password = dto.Password;
            tempentity.Name = dto.Name;
            tempentity.UserName = dto.UserName;
            await unitOfWork.Account.Update(tempentity);
        }

    }
}
