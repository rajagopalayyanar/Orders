using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrdersAPI.Models;
using OrdersAPI.ServiceRepository;

namespace OrdersAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;
        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        //[HttpGet(Name = "GetAllRestaurant")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<Order>>> GetOrders()
        {
            var orders = await _orderService.GetOrders();
            if (orders.Count == 0)
            {
                return NotFound("There is no order placed in past");
            }

            return Ok(orders);
        }

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<Order>> GetOrderAsync([FromRoute] string orderId)
        {
            var order = await _orderService.GetOrder(orderId);

            if (order != null)
            {
                return Ok(order);
            }

            else
            {
                return NotFound("No order placed");
            }
        }

        [HttpPost(Name = "CreateOrder")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public ActionResult<Order> CreateOrderAsync(Order order)
        {
            //_restaurantDBContext.Restaurant.InsertOne(restaurant);

            //return CreatedAtRoute("GetRestaurants", new { id = restaurant.RestaurentId.ToString() }, restaurant);
            if (ModelState.IsValid)
            {
                _orderService.Create(order);
                return CreatedAtRoute("GetOrders", new { id = order.OrderID.ToString() }, order);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut]
        public IActionResult UpdateOrder(string orderId, Order orderIn)
        {
            var order = _orderService.GetOrder(orderId);

            if (order == null)
            {
                return NotFound("Order is not Found");
            }

            _orderService.OrderCancel(orderId, orderIn);
            return NoContent();
        }
    }
}