using System;
using System.Threading.Tasks;
using Refactoring.Web.DomainModels;
using Refactoring.Web.Services.Interfaces;

namespace Refactoring.Web.Services
{
    public class OrderService : IOrderService
    {
        private readonly Order _order;
        private readonly IDealService _dealService;
        private readonly IChamberOfCommerceAPI _chamberOfCommerceApi;

        public OrderService(IDealService dealService, IChamberOfCommerceAPI chamberOfCommerceApi)
        {
            _dealService = dealService;
            _chamberOfCommerceApi = chamberOfCommerceApi;
        }

        public OrderService(Order order) {
            _order = order;
            _order.Id = Guid.NewGuid().ToString();
            _order.CreatedOn = DateTime.Now;
            _order.UpdatedOn = DateTime.Now;
        }

        public async Task ProcessOrder()
        {
            if (_order.District.ToLower() == "cambridge")
            {
                var advert = new Advert();
                advert.CreatedOn = DateTime.Now;
                advert.Heading = "Cambridge Bakery";
                advert.Content = "Custom Birthday and Wedding Cakes";
                if (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday) {
                    var result = await _chamberOfCommerceApi.GetFor("Middleton");
                    advert.ImageUrl = result.ThumbnailUrl;
                }
                _order.Advert = advert;
                PrintAdvert(advert, false);
                _order.Status = "Complete";
            }
            else if (_order.District.ToLower() == "middleton")
            {
                var advert = new Advert();
                advert.CreatedOn = DateTime.Now;
                var deal = _dealService.GenerateDeal(DateTime.Now);
                var biz = _dealService.GetRandomLocalBusiness();
                advert.Heading = "Middleton " + biz;
                advert.Content = "Get " + deal * 100 + "Percent off your next purchase!";
                var result = await _chamberOfCommerceApi.GetFor("Middleton");
                advert.ImageUrl = result.ThumbnailUrl;
                _order.Advert = advert;
                PrintAdvert(advert, false);
                _order.Status = "Complete";
            }
            else if (_order.District.ToLower() == "county")
            {
                var advert = new Advert();
                advert.CreatedOn = DateTime.Now;
                advert.Heading = "County Diner";
                advert.Content = "Kids eat free every Thursday night";
                _order.Advert = advert;
                PrintAdvert(advert, false);
                _order.Status = "Complete";
            }
            else if (_order.District.ToLower() == "downtown")
            {
                if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday || DateTime.Now.DayOfWeek == DayOfWeek.Sunday) {
                    PrintAdvert(null, true);
                }
                var advert = new Advert();
                advert.Heading = "Downtown Coffee Roasters";
                advert.CreatedOn = DateTime.Now;
                advert.Content = "Get a free coffee drink when you buy 1lb of coffee beans";
                _order.Advert = advert;
                PrintAdvert(advert, false);
                _order.Status = "Complete";
            }
        }

        private void PrintAdvert(Advert advert, bool IsDefaultAdvert) {
            if (IsDefaultAdvert) {
                Console.WriteLine("Printing Default Advert");
            }
            else {
                Console.WriteLine("Printing Custom Advert: " + advert.Heading);
            }
            System.Threading.Thread.Sleep(3000);
        }
    }
}