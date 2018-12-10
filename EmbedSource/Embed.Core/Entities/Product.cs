using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Embed.Core.Entities
{
    public class Product
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public double SaleAmount { get; set; }

        public void Modify(string name, int quantity, double saleAmount)
        {
            Name = name;
            Quantity = quantity;
            SaleAmount = saleAmount;
        }
    }
}
