using System.Threading.Tasks;

namespace Refactoring.Web.Services.Interfaces
{
    public interface IChamberOfCommerceAPI
    {
        Task<DataResult> GetFor(string district);
    }
}
