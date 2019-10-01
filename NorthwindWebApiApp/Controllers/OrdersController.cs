using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NorthwindWebApiApp.Models;
using NorthwindWebApiApp.Services;
using Microsoft.Extensions.Logging;	

namespace NorthwindWebApiApp.Controllers
{
    [ApiController]
	[ApiVersion("1.0")]
	[Route("api/v{version:apiVersion}/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService orderService;
		private readonly ILogger<OrdersController> logger;

        public OrdersController(IOrderService orderService, ILogger<OrdersController> logger)
        {
            this.orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
			this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

		
		/// <summary>
        /// Get all orders
        /// </summary>
		/// <returns>Sequance of orders</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BriefOrderModel>>> GetOrders()
        {
			this.logger.LogInformation("Calling OrdersController.GetOrders");
			try
			{
				return this.Ok(await this.orderService.GetOrdersAsync());
			}
			catch (Exception e)
			{
				this.logger.LogError(e, "Exception in OrdersController.GetOrders.");
				throw;
			}  
        }
		
		/// <summary>
        /// Get order by Id
        /// </summary>
		/// <param name="orderId"></param>
		/// <returns>Order</returns>
        [HttpGet("{orderId}")]
        public async Task<ActionResult<FullOrderModel>> GetOrder(int orderId)
        {
			this.logger.LogInformation("Calling OrdersController.GetOrder(id)");
			try
			{
				return this.Ok(await this.orderService.GetOrderAsync(orderId));
			}
			catch (Exception e)
			{
				this.logger.LogError(e, "Exception in OrdersController.GetOrder(id)");
				throw;
			}  
        }
    }
}
