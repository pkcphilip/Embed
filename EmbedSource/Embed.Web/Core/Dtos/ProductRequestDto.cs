using System;
using System.Collections.Generic;

namespace Embed.Web.Core.Dtos
{
    public class ProductRequestDto
    {
        public string Id { get; set; }

        public DateTime TimeStamp { get; set; }

        public IEnumerable<ProductDto> ProductDtos { get; set; }
    }
}