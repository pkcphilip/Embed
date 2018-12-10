using System;
using System.Collections.Generic;

namespace Embed.Web.Core.Dtos
{
    public class ProductResponseDto
    {
        public string Id { get; set; }

        public DateTime Timestamp { get; set; }

        public IList<ProductDto> Products { get; set; }
    }
}