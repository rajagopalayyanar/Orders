using OrdersAPI.Infrastructure;
using OrdersAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace OrdersAPI.ServiceRepository
{
    public class OrderService:IOrderService
    {
        private OrderDBContext _orderDBContext;
        public OrderService(OrderDBContext orderDBContext)
        {
            _orderDBContext = orderDBContext;
        }
        public async Task<List<OrderDetails>> GetOrders()
        {
            var orders = await _orderDBContext.OrderDetails.FindAsync(order => true);
            return orders.ToList();
        }
        public async Task<OrderDetails> GetOrder(string orderId)
        {
            var order = await _orderDBContext.OrderDetails.FindAsync<OrderDetails>(ord => ord.OrderID == orderId);
            return order.FirstOrDefault();
        }
        public void OrderCancel(string orderId, OrderDetails orderIn)
        {
            _orderDBContext.OrderDetails.ReplaceOne(ord => ord.OrderID == orderId, orderIn);
        }
        public OrderDetails Create(OrderDetails order)
        {
            _orderDBContext.OrderDetails.InsertOne(order);
            return order;
        }
    }
}
