using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.API.Models
{
    public class ProductModel
    {
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
        public string ProductCategory { get; set; }
        public string ProductImage { get; set; }
        public DateTime ProductPublishDate { get; set; }
    }

    public class ProductsModel
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
        public string ProductCategory { get; set; }
        public string ProductImage { get; set; }
    }
}
