namespace Embed.Web.Core.Dtos
{
    public class ProductDto
    {
        public long? Id { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public double SaleAmount { get; set; }
    }
}