using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.API.Models
{
    public class OrderTransectionModel
    {
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public int OrderAmount { get; set; }
    }

    public class TransectionModel
    {
        public string TransectionId { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public int OrderAmount { get; set; }
    }
   
}
