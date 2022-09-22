using AutoMapper;
using Moq;
using NUnit.Framework;
using PayCore.ProductCatalog.Application;
using PayCore.ProductCatalog.Application.Dto_Validator.Product.Dto;
using PayCore.ProductCatalog.Application.Interfaces.Repositories;
using PayCore.ProductCatalog.Application.Interfaces.UnitOfWork;
using PayCore.ProductCatalog.Application.Mapping;
using PayCore.ProductCatalog.Application.Services;
using PayCore.ProductCatalog.Domain.Entities;
using System.Threading.Tasks;

namespace PayCore.ProductCatalog.UnitTest
{
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _productRepository;
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly IMapper _mapper;

        public ProductServiceTests()
        {
            var categoryRepositoryMock = new Mock<IProductRepository>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _productRepository = new Mock<IProductRepository>();
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile())));
        }

        [Test]
        public async Task GetProduct_WithExistingItem_ReturnsExpectedItem()
        {
            _unitOfWork.Setup(u => u.Product).Returns(_productRepository.Object);
            _unitOfWork.Setup(repo => repo.Product.GetById(1)).ReturnsAsync(new Product { Id = It.IsAny<int>(), ProductName = "Test",
                                                                                            Brand = new Brand {Id = It.IsAny<int>(), BrandName = "Test" },
                                                                                            Color = new Color { Id = It.IsAny<int>(), ColorName = "Test" },
                                                                                           Category = new Category { Id = It.IsAny<int>(), CategoryName = "Test" },
                                                                                           Price = It.IsAny<int>(), IsOfferable = true,IsSold = false,Description = "Test",
                                                                                           Status = true, Owner = new Account { Id = It.IsAny<int>(), Name ="Test"} });
            var productService = new ProductService(_mapper, _unitOfWork.Object);


            var product = await productService.GetById(1);

            Assert.NotNull(product);
        }


        [Test]
        public void Insert_Product_With_Wrong_Brand_Category_ColorId_ReturnsNotFound()
        {
            //Arrange
            _unitOfWork.Setup(u => u.Product).Returns(_productRepository.Object);
            var dto = new ProductUpsertDto();
            _unitOfWork.Setup(repo => repo.Category.GetById(It.IsAny<int>())).ReturnsAsync((Category)null);
            _unitOfWork.Setup(repo => repo.Brand.GetById(It.IsAny<int>())).ReturnsAsync((Brand)null);
            _unitOfWork.Setup(repo => repo.Color.GetById(It.IsAny<int>())).ReturnsAsync((Color)null);
            var productService = new ProductService(_mapper, _unitOfWork.Object);
            
            //Act //Assert
            Assert.Throws<NotFoundException>(() => productService.InsertProduct(It.IsAny<int>(), dto).GetAwaiter().GetResult());
        }


        [Test]
        public void Remove_Product_With_Wrong_Brand_Category_ColorId_ReturnsNotFound()
        {
            //Arrange
            _unitOfWork.Setup(u => u.Product).Returns(_productRepository.Object);
            var dto = new ProductUpsertDto();
            _unitOfWork.Setup(repo => repo.Product.GetById(It.IsAny<int>())).ReturnsAsync((Product)null);
            var productService = new ProductService(_mapper, _unitOfWork.Object);

            //Act //Assert
            Assert.Throws<NotFoundException>(() => productService.Remove(It.IsAny<int>(), It.IsAny<int>()).GetAwaiter().GetResult());
        }


        [Test]
        public void Remove_ProductDoesntBelongToUser_ReturnsBadRequestException()
        {
            //Arrange 
            //As can be seen product owner id and user id does not match so bad request exception throws
            _unitOfWork.Setup(u => u.Product).Returns(_productRepository.Object);
            var dto = new ProductUpsertDto();
            _unitOfWork.Setup(repo => repo.Product.GetById(It.IsAny<int>())).ReturnsAsync(new Product
                                                                                                    {
                                                                                                        Id = It.IsAny<int>(),
                                                                                                        ProductName = "Test",
                                                                                                        Brand = new Brand { Id = It.IsAny<int>(), BrandName = "Test" },
                                                                                                        Color = new Color { Id = It.IsAny<int>(), ColorName = "Test" },
                                                                                                        Category = new Category { Id = It.IsAny<int>(), CategoryName = "Test" },
                                                                                                        Price = It.IsAny<int>(),IsOfferable = true, IsSold = false,
                                                                                                        Description = "Test",Status = true,Owner = new Account { Id = 1, Name = "Test" } //Owner id is 1
                                                                                                    });

            var productService = new ProductService(_mapper, _unitOfWork.Object);

            //Act //Assert
            Assert.Throws<BadRequestException>(() => productService.Remove(2, It.IsAny<int>()).GetAwaiter().GetResult()); //User id is 2
        }




        [Test]
        public void Buy_Product_WithNonExistingItem_ReturnsNotFound()
        {
            //Arrange
            _unitOfWork.Setup(u => u.Product).Returns(_productRepository.Object);
            _unitOfWork.Setup(repo => repo.Product.GetById(It.IsAny<int>())).ReturnsAsync((Product)null);
            var productService = new ProductService(_mapper, _unitOfWork.Object);

            //Act //Assert
            Assert.Throws<NotFoundException>(() => productService.BuyProductWithoutOffer(It.IsAny<int>(), It.IsAny<int>()).GetAwaiter().GetResult());
        }


        [Test]
        public void Buy_SoldProduct_ReturnsBadRequest()
        {
            //Arrange
            _unitOfWork.Setup(u => u.Product).Returns(_productRepository.Object);
            _unitOfWork.Setup(repo => repo.Product.GetById(It.IsAny<int>())).ReturnsAsync(new Product
            {
                Id = It.IsAny<int>(),
                ProductName = "Test",
                Brand = new Brand { Id = It.IsAny<int>(), BrandName = "Test" },
                Color = new Color { Id = It.IsAny<int>(), ColorName = "Test" },
                Category = new Category { Id = It.IsAny<int>(), CategoryName = "Test" },
                Price = It.IsAny<int>(),
                IsOfferable = true,
                IsSold = true, // Product is sold . Bad request will be thrown
                Description = "Test",
                Status = true,
                Owner = new Account { Id = 1, Name = "Test" }
            });

            var productService = new ProductService(_mapper, _unitOfWork.Object);

            //Act //Assert
            Assert.Throws<BadRequestException>(() => productService.BuyProductWithoutOffer(It.IsAny<int>(), It.IsAny<int>()).GetAwaiter().GetResult());
        }
    }
}
