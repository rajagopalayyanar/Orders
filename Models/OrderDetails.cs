using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrdersAPI.Models
{
    public class OrderDetails
    {
        public string OrderID { get; set; }
        public string DeliveryAddress { get; set; }
        public string OrderItems { get; set; }
        public double BillAmount { get; set; }
        public string PaymentMode { get; set; }
    }
}
