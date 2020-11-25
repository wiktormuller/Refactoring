﻿using Refactoring.Web.DomainModels;

namespace Refactoring.Web.Services.Interfaces
{
    public interface IAdvertPrinter
    {
        void Print(Advert advert, bool isDefaultAdvert);
    }
}
