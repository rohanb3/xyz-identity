using System.Threading.Tasks;

namespace Xyzies.SSO.Identity.Service.Service.UsersUpdatingScheduler
{
    public interface IUsersBackgroundService
    {
        Task UpdateUsers();
    }
}