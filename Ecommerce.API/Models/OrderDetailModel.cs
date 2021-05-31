using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.API.Models
{
    public class OrderDetailModel
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public int Rate { get; set; }
    }
}
