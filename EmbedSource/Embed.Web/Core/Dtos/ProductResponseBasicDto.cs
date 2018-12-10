using System;
using System.Collections.Generic;

namespace Embed.Web.Core.Dtos
{
    public class ProductResponseBasicDto
    {
        public string Id { get; set; }

        public DateTime Timestamp { get; set; }

        public IList<ProductBasicDto> Products { get; set; }
    }
}