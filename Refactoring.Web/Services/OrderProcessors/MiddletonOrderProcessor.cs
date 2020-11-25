using Refactoring.Web.DomainModels;
using Refactoring.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Refactoring.Web.Services.OrderProcessors
{
    public class MiddletonOrderProcessor
    {
        private readonly IAdvertPrinter _advertPrinter;
        private readonly IDealService _dealService;
        private readonly IChamberOfCommerceAPI _chamberOfCommerceApi;

        public MiddletonOrderProcessor(IAdvertPrinter advertPrinter, IDealService dealService, IChamberOfCommerceAPI chamberOfCommerceApi)
        {
            _advertPrinter = advertPrinter;
            _dealService = dealService;
            _chamberOfCommerceApi = chamberOfCommerceApi;
        }

        public async Task<Order> PrintAdvertAndUpdateOrder(Order order)
        {
            var result = await _chamberOfCommerceApi.GetFor("Middleton");
            var deal = _dealService.GenerateDeal(DateTime.Now);
            var biz = _dealService.GetRandomLocalBusiness();

            var advert = new Advert()
            {
                CreatedOn = DateTime.Now,
                Heading = "Middleton " + biz,
                Content = "Get " + deal * 100 + "Percent off your next purchase!",
                ImageUrl = result.ThumbnailUrl,
            };


            order.Advert = advert;
            _advertPrinter.Print(advert, false);
            order.Status = "Complete";

            return order;
        }
    }
}
