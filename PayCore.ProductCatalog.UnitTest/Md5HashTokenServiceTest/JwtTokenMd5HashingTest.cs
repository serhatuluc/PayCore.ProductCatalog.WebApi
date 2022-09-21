using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using PayCore.ProductCatalog.Application;
using PayCore.ProductCatalog.Application.Interfaces.Repositories;
using PayCore.ProductCatalog.Application.Interfaces.UnitOfWork;
using PayCore.ProductCatalog.Domain.Entities;
using PayCore.ProductCatalog.Domain.Jwt;
using PayCore.ProductCatalog.Domain.Token;
using System.Collections.Generic;

namespace PayCore.ProductCatalog.UnitTest
{
    public class JwtTokenMd5HashingTest
    {
        private readonly Mock<IAccountRepository> _accountRepository;
        private readonly Mock<IUnitOfWork> _unitOfWork;


        public JwtTokenMd5HashingTest()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _accountRepository = new Mock<IAccountRepository>();
        }

        [Test]
        public void JwtTokenMd5Test()
        {
            
            //Firstly JWTConfig is faked
            JwtConfig jwtConfig = new() { AccessTokenExpiration = 10 ,Secret = "2A49DF37289D10E73",Issuer = "Pyc",Audience = "Pyc" };
            var monitor = Mock.Of<IOptionsMonitor<JwtConfig>>(_ => _.CurrentValue == jwtConfig);

            //Token request is faked
            var _tokenRequest = new TokenRequest { UserName = "Admin", Password = "Admin123" };
         

            //Md5 hash of Admin123 generated using online resources then it will be tested if my method generates same hash
            IEnumerable<Account> accounts = new List<Account>(){ new Account {Id = 1, 
                                                                              UserName = "Admin",
                                                                              Password = "e64b78fc3bc91bcbc7dc232ba8ec59e0" , 
                                                                              Name = "Admin",Email = "Admin@gmail.com",
                                                                              Role = "Admin"}};


            _unitOfWork.Setup(u => u.Account).Returns(_accountRepository.Object);
            _unitOfWork.Setup(u => u.Account.GetAll(x => x.UserName.Equals(_tokenRequest.UserName))).ReturnsAsync(accounts);

            //Arrange
            var tokenService = new TokenService(monitor, _unitOfWork.Object);

            //Act
            var tokenResponse = tokenService.GenerateToken(_tokenRequest).GetAwaiter().GetResult();
           
            //Assert
            Assert.IsInstanceOf(typeof(TokenResponse), tokenResponse);
        }
    }
}
