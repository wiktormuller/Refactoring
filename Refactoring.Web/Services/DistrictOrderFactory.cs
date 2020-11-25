using Refactoring.Web.Services.Helpers;
using Refactoring.Web.Services.Interfaces;
using Refactoring.Web.Services.OrderProcessors;
using System;

namespace Refactoring.Web.Services
{
    public class DistrictOrderFactory : IDistrictOrderFactory
    {
        private readonly IChamberOfCommerceAPI _chamberOfCommerceApi;
        private readonly IAdvertPrinter _advertPrinter;
        private readonly IDealService _dealService;
        private readonly IDateTimeResolver _dateTimeResolver;

        public DistrictOrderFactory(IChamberOfCommerceAPI chamberOfCommerceApi, IAdvertPrinter advertPrinter, IDealService dealService, IDateTimeResolver dateTimeResolver)
        {
            _chamberOfCommerceApi = chamberOfCommerceApi;
            _advertPrinter = advertPrinter;
            _dealService = dealService;
            _dateTimeResolver = dateTimeResolver;
        }

        public OrderProcessor For(string district)
        {
            if (district.ToLower() == DistrictHelper.Cambridge)
            {
                return new CambridgeOrderProcessor(_chamberOfCommerceApi, _advertPrinter, _dateTimeResolver);
            }

            if (district.ToLower() == DistrictHelper.Middleton)
            {
                return new MiddletonOrderProcessor(_advertPrinter, _dealService, _chamberOfCommerceApi);
            }
            
            if (district.ToLower() == DistrictHelper.County)
            {
                 return new CountyOrderProcessor(_advertPrinter);
            }
            
            if (district.ToLower() == DistrictHelper.Downtown)
            {
                return new DowntownOrderProcessor(_advertPrinter);
            }

            throw new NotImplementedException($"No Processor for {district}");
        }
    }
}
