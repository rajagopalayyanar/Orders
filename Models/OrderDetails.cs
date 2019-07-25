using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace OrdersAPI.Models
{
    public class OrderDetails
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        public string OrderID { get; set; }
        public string EmailID { get; set; }
        public DateTime OrderDateTime { get; set; }
        public string RestaurantName { get; set; }
        public string OrderStatus { get; set; }
        public string DeliveryAddress { get; set; }
        public List<OrderItems> OrderItems { get; set; }
        public double BillAmount { get; set; }
        public string PaymentMode { get; set; }
    }

    public class OrderItems
    {
        public string ItemName{ get; set; }
        public double Price { get; set; }
    }
}
