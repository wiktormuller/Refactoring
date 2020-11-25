using System.Threading.Tasks;
using Amazon.Runtime.Internal.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Refactoring.Web.DomainModels;
using Refactoring.Web.Services;
using Refactoring.Web.Services.Interfaces;

namespace Refactoring.Web.Controllers {
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<OrderController> _logger;

        public OrderController(ILogger<OrderController> logger, IOrderService orderService)
        {
            _orderService = orderService;
            _logger = logger;
        }

        public IActionResult Index() {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> SubmitOrder(string selectedDistrict, decimal orderAmount)
        {
            var order = new Order
            {
                District = selectedDistrict,
                Total = orderAmount
            };
            
            order = await _orderService.ProcessOrder(order);

            if(order != null)
            {
                _logger.LogDebug($"Processed Order: {order.Id}");
                return View(order);
            }

            _logger.LogError("Error processing order");
            return StatusCode(500);
        }
    }
}