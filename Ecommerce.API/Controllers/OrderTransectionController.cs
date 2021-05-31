using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Controllers
{
    [Authorize]
    [ApiController]
    public class OrderTransectionController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public OrderTransectionController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        [Route("api/OrderTransection/List")]
        public async Task<IActionResult> GetList()
        {
            try
            {
                List<DTOModel> DTO = new List<DTOModel>();
                var Oreder = await _db.Order.ToListAsync();
                foreach (var item in Oreder)
                {
                    DTOModel Data = new DTOModel()
                    {
                        Orders = item,
                        OrderDetails = await _db.OrderDetail.Where(x => x.TransectionId == item.TransectionId).ToListAsync()
                    };
                    DTO.Add(Data);
                }
                return Ok(DTO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("api/OrderTransection/GetId")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                DTOModel Data = new DTOModel()
                {
                    Orders = await _db.Order.FirstOrDefaultAsync(x => x.TransectionId == id),
                    OrderDetails = await _db.OrderDetail.Where(x => x.TransectionId == id).ToListAsync()
                };
                return Ok(Data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut]
        [Route("api/OrderTransection/Create")]
        public async Task<IActionResult> Add([FromBody]OrderDTOModel model)
        {
            try
            {
                OrderTransectionViewModel orderTransection = new OrderTransectionViewModel()
                {
                    TransectionId = Guid.NewGuid().ToString(),
                    OrderNumber = Guid.NewGuid().ToString(),
                    Address = model.order.Address,
                    City = model.order.City,
                    State = model.order.State,
                    Country = model.order.Country,
                    OrderDate = DateTime.Now,
                    StripeToken = "Token",
                    OrderAmount = model.order.OrderAmount
                };
                _db.Order.Add(orderTransection);
                await _db.SaveChangesAsync();

                //model.orderDetail.ForEach(x => x.TransectionId = orderTransection.TransectionId);

                List<OrderDetailViewModel> OrderList = new List<OrderDetailViewModel>();

                foreach (var item in model.orderDetail)
                {
                    OrderDetailViewModel order = new OrderDetailViewModel()
                    {
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        Rate = item.Rate,
                        TotalAmount = item.Quantity * item.Rate,
                        TransectionId = orderTransection.TransectionId
                    };
                    OrderList.Add(order);
                }
                _db.OrderDetail.AddRange(OrderList);
                await _db.SaveChangesAsync();
                return Ok(new { Order = orderTransection, OrderDetail = OrderList });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut]
        [Route("api/OrderTransection/Update")]
        public async Task<IActionResult> Edit([FromBody]OrdersDTOModel model)
        {
            try
            {
                var Transection = _db.Order.FirstOrDefault(x => x.TransectionId == model.Order.TransectionId);
                Transection.TransectionId = model.Order.TransectionId;
                Transection.OrderNumber = Transection.OrderNumber;
                Transection.Address = model.Order.Address;
                Transection.City = model.Order.City;
                Transection.State = model.Order.State;
                Transection.Country = model.Order.Country;
                Transection.OrderDate = Transection.OrderDate;
                Transection.StripeToken = "Token";
                Transection.OrderAmount = model.Order.OrderAmount;
                _db.Order.Update(Transection);

                var OrderDetails = _db.OrderDetail.Where(x => x.TransectionId == model.Order.TransectionId).ToList();
                _db.OrderDetail.RemoveRange(OrderDetails);

                List<OrderDetailViewModel> OrderList = new List<OrderDetailViewModel>();

                foreach (var item in model.OrdersDetails)
                {
                    OrderDetailViewModel orderDetail = new OrderDetailViewModel()
                    {
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        Rate = item.Rate,
                        TotalAmount = item.Quantity * item.Rate,
                        TransectionId = model.Order.TransectionId
                    };
                    OrderList.Add(orderDetail);
                }
                _db.OrderDetail.AddRange(OrderList);
                await _db.SaveChangesAsync();
                return Ok(new { Order = Transection, OrderDetail = OrderList });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete]
        [Route("api/OrderTransection/Delete")]
        public async Task<IActionResult> DeleteOrder(string id)
        {
            try
            {
                var OrderDetail = _db.OrderDetail.Where(x => x.TransectionId == id).ToList();
                _db.OrderDetail.RemoveRange(OrderDetail);
                OrderTransectionViewModel orders = _db.Order.Find(id);
                _db.Order.Remove(orders);
                await _db.SaveChangesAsync();
                return Ok(new { Message = "Order is Deleted" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}