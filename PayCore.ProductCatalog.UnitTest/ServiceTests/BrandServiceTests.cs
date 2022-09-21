using AutoMapper;
using Moq;
using NUnit.Framework;
using PayCore.ProductCatalog.Application;
using PayCore.ProductCatalog.Application.Dto_Validator;
using PayCore.ProductCatalog.Application.Interfaces.Repositories;
using PayCore.ProductCatalog.Application.Interfaces.UnitOfWork;
using PayCore.ProductCatalog.Application.Mapping;
using PayCore.ProductCatalog.Application.Services;
using PayCore.ProductCatalog.Domain.Entities;
using System.Collections.Generic;

using System.Threading.Tasks;

namespace PayCore.ProductCatalog.UnitTest.ServiceTests
{
    public class BrandServiceTests
    {
        private readonly Mock<IBrandRepository> _brandRepository;
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly IMapper _mapper;

        public BrandServiceTests()
        {
            var categoryRepositoryMock = new Mock<IBrandRepository>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _brandRepository = new Mock<IBrandRepository>();
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile())));

        }

        [Test]
        public async Task GetBrands_WithExistingItem_ReturnsExpectedItem()
        {
            _unitOfWork.Setup(u => u.Brand).Returns(_brandRepository.Object);
            _unitOfWork.Setup(repo => repo.Brand.GetById(1)).ReturnsAsync(new Brand { Id = 1, BrandName = "Test" });
            var brandService = new BrandService(_mapper, _unitOfWork.Object);


            var brand = await brandService.GetById(1);

            Assert.NotNull(brand);
        }

        [Test]
        public async Task GetAllBrand_ReturnsListOfColor()
        {
            _unitOfWork.Setup(u => u.Brand).Returns(_brandRepository.Object);
            _unitOfWork.Setup(repo => repo.Brand.GetAll()).ReturnsAsync(new List<Brand>() { new Brand { Id = 1, BrandName = "Test" }, new Brand { Id = 2, BrandName = "test" } });
            var brandService = new BrandService(_mapper, _unitOfWork.Object);


            var brand = await brandService.GetAll();

            Assert.NotNull(brand);

        }

        [Test]
        public void Delete_WithNonExistingItem_ReturnNotFoundException()
        {
            //Arrange
            _unitOfWork.Setup(u => u.Brand).Returns(_brandRepository.Object);
            _unitOfWork.Setup(repo => repo.Brand.GetById(It.IsAny<int>())).ReturnsAsync((Brand)null);
            var brandService = new BrandService(_mapper, _unitOfWork.Object);

            //Act //Assert
            Assert.Throws<NotFoundException>(() => brandService.Remove(It.IsAny<int>()).GetAwaiter().GetResult());
        }


        [Test]
        public void Update_NonExistingItem_ReturnNotFound()
        {
            //Arrange
            _unitOfWork.Setup(u => u.Brand).Returns(_brandRepository.Object);
            var dto = new BrandUpsertDto();
            _unitOfWork.Setup(repo => repo.Brand.GetById(It.IsAny<int>())).ReturnsAsync((Brand)null);
            var brandService = new BrandService(_mapper, _unitOfWork.Object);

            Assert.Throws<NotFoundException>(() => brandService.Update(It.IsAny<int>(), dto).GetAwaiter().GetResult());
        }
    }
}
