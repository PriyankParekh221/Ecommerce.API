using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.API.Models
{
    [Table("OrderDetail")]
    public class OrderDetailViewModel
    {
        [Key]
        public int OrderDetailId { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public int Rate { get; set; }
        public int TotalAmount { get; set; }
        public string TransectionId { get; set; }
    }
}
