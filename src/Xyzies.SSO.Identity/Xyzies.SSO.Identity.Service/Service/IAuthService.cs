using System.Threading.Tasks;
using Xyzies.SSO.Identity.Services.Models.User;

namespace Xyzies.SSO.Identity.Services.Service
{
    public interface IAuthService
    {
        Task<TokenResponse> AuthorizeAsync(UserAuthorizeOptions options);
        Task<TokenResponse> RefreshAsync(UserRefreshOptions refresh_token);
    }
}
