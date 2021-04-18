using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quickkart.Common.Models
{
    public class Product
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public float Price { get; set; }
        public int QuantityAvailable { get; set; }
    }
}
