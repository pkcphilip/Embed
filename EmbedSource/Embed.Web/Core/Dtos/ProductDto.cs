using System.ComponentModel.DataAnnotations;

namespace Embed.Web.Core.Dtos
{
    public class ProductDto
    {
        [RegularExpression("^-?\\d+$", ErrorMessage = "Id should be numeric.")]
        [Range(1, long.MaxValue)]
        public long? Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [RegularExpression("^-?\\d+$", ErrorMessage = "Quantity should be numeric.")]
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        [Required]
        [Range(0.01, int.MaxValue)]
        public double SaleAmount { get; set; }
    }
}