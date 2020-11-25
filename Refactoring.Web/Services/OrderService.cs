using System;
using System.Threading.Tasks;
using Refactoring.Web.DomainModels;
using Refactoring.Web.Services.Interfaces;
using Refactoring.Web.Services.OrderProcessors;

namespace Refactoring.Web.Services
{
    public class OrderService : IOrderService
    {
        private readonly IDealService _dealService;
        private readonly IChamberOfCommerceAPI _chamberOfCommerceApi;
        private readonly IAdvertPrinter _advertPrinter;

        public OrderService(IDealService dealService, IChamberOfCommerceAPI chamberOfCommerceApi, IAdvertPrinter advertPrinter)
        {
            _dealService = dealService;
            _chamberOfCommerceApi = chamberOfCommerceApi;
            _advertPrinter = advertPrinter;
        }

        public async Task<Order> ProcessOrder(Order order)
        {
            order.Id = Guid.NewGuid().ToString();
            order.CreatedOn = DateTime.Now;
            order.UpdatedOn = DateTime.Now;


            if (order.District.ToLower() == "cambridge")
            {
                var orderProcessor = new CambridgeOrderProcessor(_chamberOfCommerceApi, _advertPrinter);
                await orderProcessor.PrintAdvertAndUpdateOrder(order);
            }
            else if (order.District.ToLower() == "middleton")
            {
                var orderProcessor = new MiddletonOrderProcessor(_advertPrinter, _dealService, _chamberOfCommerceApi);
                await orderProcessor.PrintAdvertAndUpdateOrder(order);
            }
            else if (order.District.ToLower() == "county")
            {
                var orderProcessor = new CountyOrderProcessor(_advertPrinter);
                await orderProcessor.PrintAdvertAndUpdateOrder(order);
            }
            else if (order.District.ToLower() == "downtown")
            {
                var orderProcessor = new DowntownOrderProcessor(_advertPrinter);
                await orderProcessor.PrintAdvertAndUpdateOrder(order);
            }

            return order;
        }
    }
}