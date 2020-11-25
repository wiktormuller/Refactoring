using System.Threading.Tasks;

namespace Refactoring.Web.Services.Interfaces
{
    public interface IChamberOfCommerceAPI
    {
        Task<DataResult> GetImageAndThumbnailDataFor(string district);
    }
}
