using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NorthwindWebApiApp.Models;
using NorthwindWebApiApp.Services;

namespace NorthwindWebApiApp.Controllers
{
    [ApiController]
	[ApiVersion("1.0")]
	[Route("api/v{version:apiVersion}/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService orderService;

        public OrdersController(IOrderService orderService)
        {
            this.orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
        }

		
		/// <summary>
        /// Get all orders
        /// </summary>
		/// <returns>Sequance of orders</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BriefOrderModel>>> GetOrders()
        {
            return this.Ok(await this.orderService.GetOrdersAsync());
        }
		
		/// <summary>
        /// Get order by Id
        /// </summary>
		/// <param name="orderId"></param>
		/// <returns>Order</returns>
        [HttpGet("{orderId}")]
        public async Task<ActionResult<FullOrderModel>> GetOrder(int orderId)
        {
            return this.Ok(await this.orderService.GetOrderAsync(orderId));
        }
    }
}
