using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.API.Models
{
    public class DTOModel
    {
        public OrderTransectionViewModel Orders { get; set; }
        public List<OrderDetailViewModel> OrderDetails { get; set; }
    }

    public class OrderDTOModel
    {
        public OrderTransectionModel order { get; set; }
        public List<OrderDetailModel> orderDetail { get; set; }
    }

    public class OrdersDTOModel
    {
        public TransectionModel Order { get; set; }
        public List<OrderDetailModel> OrdersDetails { get; set; }
    }
}
