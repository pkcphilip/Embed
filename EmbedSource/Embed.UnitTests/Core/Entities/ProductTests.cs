using System;
using Embed.Core.Entities;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Embed.UnitTests.Core.Entities
{
    [TestClass]
    public class ProductTests
    {
        [TestMethod]
        public void Modify_WhenCalled_ShouldUpdateCorrespondingFields()
        {
            var product = new Product
            {
                Name = "Retro Aracade Machine",
                Quantity = 10,
                SaleAmount = 50.00
            };

            var newName = "Retro Aracade Machine VII";
            var newQuantity = 5;
            var newSaleAmount = 4.00;

            product.Modify(newName, newQuantity, newSaleAmount);

            product.Name.Should().Be(newName);
            product.Quantity.Should().Be(newQuantity);
            product.SaleAmount.Should().Be(newSaleAmount);
        }
    }
}
