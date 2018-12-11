using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Embed.Core.Models;
using Embed.Persistance.Repositories.Auth;
using Embed.Web.Controllers.Api;
using FluentAssertions;
using Microsoft.AspNet.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Embed.UnitTests.Web.Controllers
{
    [TestClass]
    public class AccountControllerTests
    {
        private AccountController _controller;
        private Mock<IAuthRepository> _mockRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepository = new Mock<IAuthRepository>();

            _controller = new AccountController(_mockRepository.Object) { Configuration = new HttpConfiguration() };
        }


        [TestMethod]
        public async Task Register_WithoutUserName_ReturnsInvalidModelState()
        {
            // Arrange
            var user = new UserModel
            {
                UserName = null,
                Password = "TestPassword123456",
                ConfirmPassword = "TestPassword123456"
            };

            // Act
            _controller.Validate(user);
            var result = await _controller.Register(user);

            // Assert
            result.Should().BeOfType<InvalidModelStateResult>();
        }

        [TestMethod]
        public async Task Register_WithoutPassword_ReturnsInvalidModelState()
        {
            // Arrange
            var user = new UserModel
            {
                UserName = "TestUsername",
                Password = null,
                ConfirmPassword = "TestPassword123456"
            };

            // Act
            _controller.Validate(user);
            var result = await _controller.Register(user);

            // Assert
            result.Should().BeOfType<InvalidModelStateResult>();
        }

        [TestMethod]
        public async Task Register_WithoutConfirmPassword_ReturnsInvalidModelState()
        {
            // Arrange
            var user = new UserModel
            {
                UserName = "TestUsername",
                Password = "TestPassword123456",
                ConfirmPassword = null
            };

            // Act
            _controller.Validate(user);
            var result = await _controller.Register(user);

            // Assert
            result.Should().BeOfType<InvalidModelStateResult>();
        }

        [TestMethod]
        public async Task Register_WithTooShortPassword_ReturnsInvalidModelState()
        {
            // Arrange
            var user = new UserModel
            {
                UserName = "TestUsername",
                Password = "123",
                ConfirmPassword = "123"
            };

            // Act
            _controller.Validate(user);
            var result = await _controller.Register(user);

            // Assert
            result.Should().BeOfType<InvalidModelStateResult>();
        }

        [TestMethod]
        public async Task Register_WithDifferingPasswordAndConfirmPassword_ReturnsInvalidModelState()
        {
            // Arrange
            var user = new UserModel
            {
                UserName = "TestUsername",
                Password = "TestPassword123456",
                ConfirmPassword = "Password123456"
            };

            // Act
            _controller.Validate(user);
            var result = await _controller.Register(user);

            // Assert
            result.Should().BeOfType<InvalidModelStateResult>();
        }

        [TestMethod]
        public async Task Register_WithValidModel_CreatesUser()
        {
            var user = new UserModel
            {
                UserName = "TestUsername",
                Password = "TestPassword123456",
                ConfirmPassword = "TestPassword123456"
            };

            _mockRepository.Setup(r => r.RegisterUser(user)).ReturnsAsync(IdentityResult.Success);

            _controller.Validate(user);
            var result = await _controller.Register(user);

            result.Should().BeOfType<OkResult>();
        }
    }
}
