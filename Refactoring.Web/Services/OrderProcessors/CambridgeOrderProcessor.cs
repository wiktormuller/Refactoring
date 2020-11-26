using Refactoring.Web.DomainModels;
using Refactoring.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Refactoring.Web.Services.OrderProcessors
{
    public class CambridgeOrderProcessor : OrderProcessor
    {
        private readonly IChamberOfCommerceAPI _chamberOfCommerceApi;
        private readonly IAdvertPrinter _advertPrinter;
        private readonly IDateTimeResolver _dateResolver;

        public CambridgeOrderProcessor(IChamberOfCommerceAPI chamberOfCommerceApi, IAdvertPrinter advertPrinter, IDateTimeResolver dateResolver)
        {
            _chamberOfCommerceApi = chamberOfCommerceApi;
            _advertPrinter = advertPrinter;
            _dateResolver = dateResolver;
        }

        public override async Task<Order> PrintAdvertAndUpdateOrder(Order order)
        {
            var advert = new Advert()
            {
                CreatedOn = DateTime.Now,
                Heading = "Cambridge Bakery",
                Content = "Custom Birthday and Wedding Cakes"
            };

            if (_dateResolver.IsItTuesday())
            {
                var result = await _chamberOfCommerceApi.GetImageAndThumbnailDataFor("Middleton");
                advert.ImageUrl = result.ThumbnailUrl;
            }

            order.Advert = advert;
            _advertPrinter.PrintCustom(advert);
            order.Status = "Complete";

            return order;
        }
    }
}
