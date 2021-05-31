using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.API.Models
{
    [Table("OrderTransection")]
    public class OrderTransectionViewModel
    {
        [Key]
        public string TransectionId { get; set; }
        public string OrderNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public DateTime OrderDate { get; set; }
        public string StripeToken { get; set; }
        public int OrderAmount { get; set; }
    }
}
