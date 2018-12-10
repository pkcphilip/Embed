using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Embed.Persistance.Repositories;
using Moq;
using System.Data.Entity;
using Embed.Core.Entities;
using Embed.Persistance;
using FluentAssertions;
using FluentAssertions.Collections;
using Embed.UnitTests.Extensions;
using System.Threading.Tasks;

namespace Embed.UnitTests.Persistance.Repositories
{
    [TestClass]
    public class ProductRepositoryTests
    {
        private ProductRepository _repository;
        private Mock<DbSet<Product>> _mockProducts;
        private Mock<IApplicationDbContext> _context;


        [TestInitialize]
        public void TestInitialize()
        {
            _mockProducts = new Mock<DbSet<Product>>();

            _context = new Mock<IApplicationDbContext>();
            _context.SetupGet(c => c.Products).Returns(() => _mockProducts.Object);

            _repository = new ProductRepository(_context.Object);
        }

        [TestMethod]
        public void GetProducts_DefaultListOfProducts_ShouldReturnAllProducts()
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

            _mockProducts.SetSource(sourceProducts);

            var products = _repository.GetAllProducts();

            products.Should().BeEquivalentTo(sourceProducts);
        }

        [TestMethod]
        public void GetProducts_EmptyListOfProducts_ShouldBeEmpty()
        {
            var sourceProducts = new List<Product>();

            _mockProducts.SetSource(sourceProducts);

            var products =  _repository.GetAllProducts();

            products.Should().BeEmpty();
        }

        [TestMethod]
        public void GetProduct_ValidProductId_ShouldReturnProduct()
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

            _mockProducts.SetSource(sourceProducts);

            var product = _repository.GetProduct(1);

            product.Should().BeEquivalentTo(sourceProducts[0]);
            product.Name.Should().Be(sourceProducts[0].Name);
            product.Quantity.Should().Be(sourceProducts[0].Quantity);
            product.SaleAmount.Should().Be(sourceProducts[0].SaleAmount);
        }

        public void GetProduct_InvalidProductId_ShouldReturnNull()
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

            _mockProducts.SetSource(sourceProducts);

            var product = _repository.GetProduct(99);

            product.Should().BeNull();
        }

        [TestMethod]
        public void GetProducts_ValidListOfProductIds_ShouldReturnProducts()
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

            _mockProducts.SetSource(sourceProducts);

            var products = _repository.GetProductsByIds(new List<long> { 4, 5, 6 });

            products.Should().HaveCount(3);
            products.Should().BeEquivalentTo(sourceProducts[3], sourceProducts[4], sourceProducts[5]);
        }

        [TestMethod]
        public void GetProducts_ListOfProductIdsIsEmpty_ShouldReturnNull()
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

            _mockProducts.SetSource(sourceProducts);

            var products = _repository.GetProductsByIds(new List<long> ());

            products.Should().BeNull();
        }


        [TestMethod]
        public void GetProducts_ListOfProductIdsIsNull_ShouldReturnNull()
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

            _mockProducts.SetSource(sourceProducts);

            var products = _repository.GetProductsByIds(null);

            products.Should().BeNull();
        }


        [TestMethod]
        public void Add_Product_CalledContextProductAddMethodOnce()
        {
            var product = new Product
            {
                Name = "Retro Aracade Machine",
                Quantity = 10,
                SaleAmount = 50.00
            };

            _context.Setup(c => c.Products.Add(product));
            _mockProducts.Setup(p => p.Add(It.IsAny<Product>())).Returns(product);

            _repository.Add(product);

            _context.Verify(c => c.Products.Add(product), Times.Once);
        }
    }
}
