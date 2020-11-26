using Refactoring.Web.DomainModels;
using Refactoring.Web.Services.Helpers;
using Refactoring.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Refactoring.Web.Services.OrderProcessors
{
    public class MiddletonOrderProcessor : OrderProcessor
    {
        private readonly IAdvertPrinter _advertPrinter;
        private readonly IDealService _dealService;
        private readonly IChamberOfCommerceAPI _chamberOfCommerceApi;
        private readonly IRandomHelper _randomHelper;

        public MiddletonOrderProcessor(IAdvertPrinter advertPrinter, IDealService dealService, IChamberOfCommerceAPI chamberOfCommerceApi, IRandomHelper randomHelper)
        {
            _advertPrinter = advertPrinter;
            _dealService = dealService;
            _chamberOfCommerceApi = chamberOfCommerceApi;
            _randomHelper = randomHelper;
        }

        public override async Task<Order> PrintAdvertAndUpdateOrder(Order order)
        {
            var result = await _chamberOfCommerceApi.GetImageAndThumbnailDataFor("Middleton");
            var deal = _dealService.GenerateDeal(DateTime.Now);
            var biz = _randomHelper.GetRandomValue(BusinessHelper.AllBusinesses);

            var advert = new Advert()
            {
                CreatedOn = DateTime.Now,
                Heading = "Middleton " + biz,
                Content = "Get " + deal * 100 + "Percent off your next purchase!",
                ImageUrl = result.ThumbnailUrl,
            };


            order.Advert = advert;
            _advertPrinter.PrintCustom(advert);
            order.Status = "Complete";

            return order;
        }
    }
}
