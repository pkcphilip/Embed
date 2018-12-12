namespace Embed.Core.Entities
{
    /// <summary>
    /// Represents the product class to hold relevant product information.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Gets or sets the product id.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the product name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the product quantity.
        /// </summary>
        public int Quantity { get; set; }


        /// <summary>
        /// Gets or sets the sale amount.
        /// </summary>
        public double SaleAmount { get; set; }

        /// <summary>
        /// To modify the product information.
        /// </summary>
        /// <param name="name">The name value to be updated.</param>
        /// <param name="quantity">The quantity value to be updated.</param>
        /// <param name="saleAmount">The sale amount value to be updated.</param>
        public void Modify(string name, int quantity, double saleAmount)
        {
            Name = name;
            Quantity = quantity;
            SaleAmount = saleAmount;
        }
    }
}
