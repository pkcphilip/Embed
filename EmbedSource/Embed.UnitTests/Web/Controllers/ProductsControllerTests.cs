using System;
using System.Collections.Generic;
using System.Web.Http.Results;
using AutoMapper;
using Embed.Core.Abstract;
using Embed.Core.Entities;
using Embed.Persistance.Repositories;
using Embed.Web.Controllers.Api;
using Embed.Web.Core.Dtos;
using Embed.Web.Mapping;
using FluentAssertions;
using FluentAssertions.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Embed.UnitTests.Web.Controllers
{
    [TestClass]
    public class ProductsControllerTests
    {
        private ProductsController _controller;
        private Mock<IProductRepository> _mockRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepository = new Mock<IProductRepository>();

            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.SetupGet(u => u.Products).Returns(_mockRepository.Object);

            _controller = new ProductsController(mockUoW.Object);
            _controller.Request = new System.Net.Http.HttpRequestMessage();

            Mapper.Reset();
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

        }

        [TestMethod]
        public void GetProducts_ValidRequest_ShouldReturnOk()
        {
            var sourceProducts = new List<Product>()
            {
                new Product { Id = 1, Name = "Stand Up Arcade Machine", Quantity = 5, SaleAmount = 3200.00 },
                new Product { Id = 2, Name = "Street Fighter 2", Quantity = 50, SaleAmount = 11.25 },
                new Product { Id = 3, Name = "Pinball Machine", Quantity = 10, SaleAmount = 1500.00 },
                new Product { Id = 4, Name = "Card Scanner", Quantity = 150, SaleAmount = 12.00 },
                new Product { Id = 5, Name = "MotoGP MotorCycle", Quantity = 3, SaleAmount = 1300.00 },
                new Product { Id = 6, Name = "Street Basketball Arcade Machine", Quantity = 8, SaleAmount = 700.00 },
            };

            _mockRepository.Setup(r => r.GetAllProducts()).Returns(sourceProducts);

            var result = _controller.GetProducts();

            result.Should().BeOfType<OkNegotiatedContentResult<ProductResponseBasicDto>>();
        }

        [TestMethod]
        public void GetProduct_ValidProductId_ShouldReturnOk()
        {
            var sourceProduct = new Product { Id = 1, Name = "Stand Up Arcade Machine", Quantity = 5, SaleAmount = 3200.00 };

            _mockRepository.Setup(r => r.GetProduct(1)).Returns(sourceProduct);

            var result = _controller.GetProduct(1);

            result.Should().BeOfType<OkNegotiatedContentResult<ProductResponseBasicDto>>();
        }


        [TestMethod]
        public void GetProduct_InvalidProductId_ShouldReturnNotFound()
        {
            _mockRepository.Setup(r => r.GetProduct(1)).Returns(((Product)null));

            var result = _controller.GetProduct(1);

            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void GetProducts_ValidProductIdsString_ShouldReturnOk()
        {
            var sourceProducts = new List<Product>()
            {
                new Product { Id = 1, Name = "Stand Up Arcade Machine", Quantity = 5, SaleAmount = 3200.00 },
                new Product { Id = 2, Name = "Street Fighter 2", Quantity = 50, SaleAmount = 11.25 },
                new Product { Id = 3, Name = "Pinball Machine", Quantity = 10, SaleAmount = 1500.00 }
            };

            var productIds = "1, 2, 3";

            _mockRepository.Setup(r => r.GetProductsByIds(new List<long> { 1, 2, 3 })).Returns(sourceProducts);

            var result = _controller.GetProducts(productIds);

            result.Should().BeOfType<OkNegotiatedContentResult<ProductResponseBasicDto>>();
        }

        [TestMethod]
        public void GetProducts_InvalidProductIdsFormat_ShouldThrowBadRequest()
        {
            var productIds = "a, b, 3";

            var result = _controller.GetProducts(productIds);

            result.Should().BeOfType<BadRequestErrorMessageResult>();
        }

        [TestMethod]
        public void GetProducts_InvalidProductIdsOverflow_ShouldThrowBadRequest()
        {
            var productIds = "1, 2, 9999999999999999999999999999999999999999999999999999999999999999999999";

            var result = _controller.GetProducts(productIds);

            result.Should().BeOfType<BadRequestErrorMessageResult>();
        }

        [TestMethod]
        public void PutNewProduct_ValidProductWithoutProductId_ShouldReturnOk()
        {
            var dto = new ProductDto
            {
                Name = "Stand Up Arcade Machine",
                Quantity = 5,
                SaleAmount = 3200.00
            };

            var result = _controller.PutNewProduct(dto);

            result.Should().BeOfType<OkNegotiatedContentResult<ProductResponseDto>>();
        }

        [TestMethod]
        public void PutProduct_ValidProductIdAndBody_ShouldReturnOk()
        {
            var dto = new ProductDto
            {
                Id = 1,
                Name = "Stand Up Arcade Machine",
                Quantity = 5,
                SaleAmount = 3200.00
            };

            var product = new Product
            {
                Id = 1,
                Name = "Stand Up Arcade Machine",
                Quantity = 5,
                SaleAmount = 3200.00

            };

            _mockRepository.Setup(r => r.GetProduct(1)).Returns(product);

            var result = _controller.PutProduct(1, dto);

            result.Should().BeOfType<OkNegotiatedContentResult<ProductResponseDto>>();
        }

        [TestMethod]
        public void PutProduct_ValidProductIdAndBody_ShouldReturnNotFound()
        {
            var dto = new ProductDto
            {
                Id = 1,
                Name = "Stand Up Arcade Machine",
                Quantity = 5,
                SaleAmount = 3200.00
            };

            var product = new Product
            {
                Id = 1,
                Name = "Stand Up Arcade Machine",
                Quantity = 5,
                SaleAmount = 3200.00

            };

            _mockRepository.Setup(r => r.GetProduct(1)).Returns(product);

            var result = _controller.PutProduct(1, dto);

            result.Should().BeOfType<OkNegotiatedContentResult<ProductResponseDto>>();
        }

        [TestMethod]
        public void PutProducts_MultipleProductDtos_ShouldReturnOk()
        {

            var sourceProductDtos = new List<ProductDto>()
            {
                new ProductDto { Id = 1, Name = "Stand Up Arcade Machine", Quantity = 5, SaleAmount = 3200.00 },
                new ProductDto { Id = 2, Name = "Street Fighter 2", Quantity = 50, SaleAmount = 11.25 },
                new ProductDto { Id = 3, Name = "Pinball Machine", Quantity = 10, SaleAmount = 1500.00 },
                new ProductDto { Name = "Card Scanner", Quantity = 150, SaleAmount = 12.00 },
                new ProductDto { Name = "MotoGP MotorCycle", Quantity = 3, SaleAmount = 1300.00 },
                new ProductDto { Name = "Street Basketball Arcade Machine", Quantity = 8, SaleAmount = 700.00 },
            };

            _mockRepository.Setup(r => r.GetProduct(1)).Returns(new Product());

            var result = _controller.PutProducts(sourceProductDtos);
            result.Should().BeOfType<OkNegotiatedContentResult<ProductResponseDto>>();
        }

        [TestMethod]
        public void PutProducts_EmptyProductDtos_ShouldReturnBadRequest()
        {
            var sourceProductDtos = new List<ProductDto>();

            var result = _controller.PutProducts(sourceProductDtos);

            result.Should().BeOfType<BadRequestErrorMessageResult>();
        }
    }
}
