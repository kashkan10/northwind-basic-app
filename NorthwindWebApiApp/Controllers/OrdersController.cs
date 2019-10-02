using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<OrdersController> logger;
        private readonly IMapper mapper;

        public OrdersController(IOrderService orderService, ILogger<OrdersController> logger, IMapper mapper)
        {
            this.orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Get all orders.
        /// </summary>
        /// <returns>Sequance of orders.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BriefOrderModel>>> GetOrders()
        {
            this.logger.LogInformation("Calling OrdersController.GetOrders");
            try
            {
                var result = await this.orderService.GetOrdersAsync();
                return this.Ok(this.mapper.Map<BriefOrderModel[]>(result));
            }
            catch (Exception e)
            {
                this.logger.LogError(e, "Exception in OrdersController.GetOrders.");
                throw;
            }
        }

        /// <summary>
        /// Get order by Id.
        /// </summary>
        /// <param name="orderId">Id of order.</param>
        /// <returns>Order.</returns>
        [HttpGet("{orderId}")]
        public async Task<ActionResult<FullOrderModel>> GetOrder(int orderId)
        {
            this.logger.LogInformation("Calling OrdersController.GetOrder(id)");
            try
            {
                var result = await this.orderService.GetOrderAsync(orderId);
                return this.Ok(this.mapper.Map<FullOrderModel>(result));
            }
            catch (Exception e)
            {
                this.logger.LogError(e, "Exception in OrdersController.GetOrder(id)");
                throw;
            }
        }
    }
}
