using Refactoring.Web.DomainModels;
using System;

namespace Refactoring.Web.Services.Printers
{
    public class AdvertPrinter
    {
        public void Print(Advert advert, bool IsDefaultAdvert)
        {
            if (IsDefaultAdvert)
            {
                Console.WriteLine("Printing Default Advert");
            }
            else
            {
                Console.WriteLine("Printing Custom Advert: " + advert.Heading);
            }
            System.Threading.Thread.Sleep(3000);
        }
    }
}
