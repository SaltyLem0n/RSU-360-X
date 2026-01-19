using RSU_360_X.Models.Auth;

namespace RSU_360_X.Services
{
    public interface IHybridAuthService
    {
        Task<AppUser?> LoginAsync(string username, string password);
    }
}
