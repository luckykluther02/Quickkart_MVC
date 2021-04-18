using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quickkart.Common.Models
{
    public class ViewCart
    {
        public string ProductName { get; set; }
        public float Price { get; set; }
        public int QuantityPurchased { get; set; }
        public float TotalAmount { get; set; }
        public string OrderId { get; set; }
        public string OrderDate { get; set; }
    }
}
