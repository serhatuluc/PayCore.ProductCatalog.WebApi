using AutoMapper;
using Moq;
using NUnit.Framework;
using PayCore.ProductCatalog.Application;
using PayCore.ProductCatalog.Application.Dto_Validator.Account.Dto;
using PayCore.ProductCatalog.Application.Interfaces.Repositories;
using PayCore.ProductCatalog.Application.Interfaces.UnitOfWork;
using PayCore.ProductCatalog.Application.Mapping;
using PayCore.ProductCatalog.Application.Services;
using PayCore.ProductCatalog.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PayCore.ProductCatalog.UnitTest.ServiceTests
{
    public class AccountServiceTests
    {
        private readonly Mock<IAccountRepository> _accountRepository;
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly IMapper _mapper;

        public AccountServiceTests()
        {
            var categoryRepositoryMock = new Mock<IAccountRepository>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _accountRepository = new Mock<IAccountRepository>();
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile())));

        }

        [Test]
        public async Task GetAccount_WithExistingItem_ReturnsExpectedItem()
        {
            _unitOfWork.Setup(u => u.Account).Returns(_accountRepository.Object);
            _unitOfWork.Setup(repo => repo.Account.GetById(1)).ReturnsAsync(new Account { Id = 1, Name = "Test" });
            var accountService = new AccountService(_mapper, _unitOfWork.Object);


            var color = await accountService.GetById(1);

            Assert.NotNull(color);
        }

        [Test]
        public async Task GetAllAccount_ReturnsListOfAccounts()
        {
            _unitOfWork.Setup(u => u.Account).Returns(_accountRepository.Object);
            _unitOfWork.Setup(repo => repo.Account.GetAll()).ReturnsAsync(new List<Account>() { new Account { Id = 1, Name = "Test" }, new Account { Id = 2, Name = "test" } });
            var accountService = new AccountService(_mapper, _unitOfWork.Object);


            var account = await accountService.GetAll();

            Assert.NotNull(account);

        }

        [Test]
        public void Delete_WithNonExistingItem_ReturnNotFoundException()
        {
            //Arrange
            _unitOfWork.Setup(u => u.Account).Returns(_accountRepository.Object);
            _unitOfWork.Setup(repo => repo.Account.GetById(It.IsAny<int>())).ReturnsAsync((Account)null);
            var accountService = new AccountService(_mapper, _unitOfWork.Object);

            //Act //Assert
            Assert.Throws<NotFoundException>(() => accountService.Remove(It.IsAny<int>()).GetAwaiter().GetResult());
        }


        [Test]
        public void Update_NonExistingItem_ReturnNotFound()
        {
            //Arrange
            _unitOfWork.Setup(u => u.Account).Returns(_accountRepository.Object);
            var dto = new AccountUpsertDto();
            _unitOfWork.Setup(repo => repo.Account.GetById(It.IsAny<int>())).ReturnsAsync((Account)null);
            var accountService = new AccountService(_mapper, _unitOfWork.Object);

            Assert.Throws<NotFoundException>(() => accountService.Update(It.IsAny<int>(), dto).GetAwaiter().GetResult());
        }
    }
}
