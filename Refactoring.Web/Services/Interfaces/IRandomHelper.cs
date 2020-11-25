using System.Collections.Generic;

namespace Refactoring.Web.Services.Interfaces
{
    public interface IRandomHelper
    {
        T GetRandomValue<T>(IEnumerable<T> items);
    }
}
