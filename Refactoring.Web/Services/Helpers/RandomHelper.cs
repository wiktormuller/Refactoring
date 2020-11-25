using Refactoring.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Refactoring.Web.Services.Helpers
{
    public class RandomHelper : IRandomHelper
    {
        public T GetRandomValue<T>(IEnumerable<T> items)
        {
            var random = new Random();
            var enumerable = items.ToList();
            var randomIndex = random.Next(enumerable.Count);

            return enumerable[randomIndex];
        }
    }
}
