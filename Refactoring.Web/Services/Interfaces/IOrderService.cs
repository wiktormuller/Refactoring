using Refactoring.Web.DomainModels;
using System.Threading.Tasks;

namespace Refactoring.Web.Services.Interfaces
{
    public interface IOrderService
    {
        Task ProcessTask();
        Order GetOrder();
    }
}
