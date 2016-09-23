using System.Threading.Tasks;

namespace PrismMobileApp.Models
{
    public interface IDataSynchronizer
    {
        Task SyncAsync();
    }
}
