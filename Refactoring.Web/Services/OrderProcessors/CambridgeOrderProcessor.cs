using Refactoring.Web.DomainModels;
using Refactoring.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Refactoring.Web.Services.OrderProcessors
{
    public class CambridgeOrderProcessor
    {
        private readonly IChamberOfCommerceAPI _chamberOfCommerceApi;
        private readonly IAdvertPrinter _advertPrinter;

        public CambridgeOrderProcessor(IChamberOfCommerceAPI chamberOfCommerceApi, IAdvertPrinter advertPrinter)
        {
            _chamberOfCommerceApi = chamberOfCommerceApi;
            _advertPrinter = advertPrinter;
        }

        public async Task<Order> PrintAdvertAndUpdateOrder(Order order)
        {
            var advert = new Advert()
            {
                CreatedOn = DateTime.Now,
                Heading = "Cambridge Bakery",
                Content = "Custom Birthday and Wedding Cakes"
            };

            if (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday)
            {
                var result = await _chamberOfCommerceApi.GetFor("Middleton");
                advert.ImageUrl = result.ThumbnailUrl;
            }

            order.Advert = advert;
            _advertPrinter.Print(advert, false);
            order.Status = "Complete";

            return order;
        }
    }
}
