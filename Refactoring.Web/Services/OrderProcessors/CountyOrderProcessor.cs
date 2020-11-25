﻿using Refactoring.Web.DomainModels;
using Refactoring.Web.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace Refactoring.Web.Services.OrderProcessors
{
    public class CountyOrderProcessor : OrderProcessor
    {
        private readonly IAdvertPrinter _advertPrinter;

        public CountyOrderProcessor(IAdvertPrinter advertPrinter)
        {
            _advertPrinter = advertPrinter;
        }

        public override async Task<Order> PrintAdvertAndUpdateOrder(Order order)
        {
            var advert = new Advert()
            {
                CreatedOn = DateTime.Now,
                Heading = "County Diner",
                Content = "Kids eat free every Thursday night"
            };
            
            order.Advert = advert;
            _advertPrinter.Print(advert, false);
            order.Status = "Complete";

            return order;
        }
    }
}
