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
    public class ColorServiceTests
    {
        private readonly Mock<IColorRepository> _colorRepository;
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly IMapper _mapper;

        public ColorServiceTests()
        {
            var categoryRepositoryMock = new Mock<IColorRepository>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _colorRepository = new Mock<IColorRepository>();
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile())));

        }

        [Test]
        public async Task GetColor_WithExistingItem_ReturnsExpectedItem()
        {
            _unitOfWork.Setup(u => u.Color).Returns(_colorRepository.Object);
            _unitOfWork.Setup(repo => repo.Color.GetById(1)).ReturnsAsync(new Color{ Id = 1, ColorName = "Test" });
            var colorService = new ColorService(_mapper, _unitOfWork.Object);


            var color = await colorService.GetById(1);

            Assert.NotNull(color);
        }

        [Test]
        public async Task GetAllColor_ReturnsListOfColor()
        {
            _unitOfWork.Setup(u => u.Color).Returns(_colorRepository.Object);
            _unitOfWork.Setup(repo => repo.Color.GetAll()).ReturnsAsync(new List<Color>() { new Color { Id = 1, ColorName = "Test" }, new Color { Id = 2, ColorName = "test" } });
            var colorService = new ColorService(_mapper, _unitOfWork.Object);


            var color = await colorService.GetAll();

            Assert.NotNull(color);

        }

        [Test]
        public void Delete_WithNonExistingItem_ReturnNotFoundException()
        {
            //Arrange
            _unitOfWork.Setup(u => u.Color).Returns(_colorRepository.Object);
            _unitOfWork.Setup(repo => repo.Color.GetById(It.IsAny<int>())).ReturnsAsync((Color)null);
            var colorService = new ColorService(_mapper, _unitOfWork.Object);

            //Act //Assert
            Assert.Throws<NotFoundException>(() => colorService.Remove(It.IsAny<int>()).GetAwaiter().GetResult());
        }


        [Test]
        public void Update_NonExistingItem_ReturnNotFound()
        {
            //Arrange
            _unitOfWork.Setup(u => u.Color).Returns(_colorRepository.Object);
            var dto = new ColorUpsertDto();
            _unitOfWork.Setup(repo => repo.Color.GetById(It.IsAny<int>())).ReturnsAsync((Color)null);
            var colorService = new ColorService(_mapper, _unitOfWork.Object);

            Assert.Throws<NotFoundException>(() => colorService.Update(It.IsAny<int>(), dto).GetAwaiter().GetResult());
        }
    }
}
