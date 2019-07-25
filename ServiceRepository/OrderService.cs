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
        public async Task<List<Order>> GetOrders()
        {
            var orders = await _orderDBContext.Order.FindAsync(order => true);
            return orders.ToList();
        }
        public async Task<Order> GetOrder(string orderId)
        {
            var order = await _orderDBContext.Order.FindAsync<Order>(ord => ord.OrderID == orderId);
            return order.FirstOrDefault();
        }
        public void OrderCancel(string orderId, Order orderIn)
        {
            _orderDBContext.Order.ReplaceOne(ord => ord.OrderID == orderId, orderIn);
        }
        public Order Create(Order order)
        {
            _orderDBContext.Order.InsertOne(order);
            return order;
        }
    }
}
