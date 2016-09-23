using System.Threading.Tasks;

namespace PrismMobileApp.Models
{
    public interface IAuthenticator
    {
        Task<bool> AuthenticateAsync();
    }
}
