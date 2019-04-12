using System.Threading.Tasks;

namespace Xyzies.SSO.Identity.Services.Service.ResetPassword
{
    public interface IResetPasswordService
    {
        Task SendConfirmationCodeAsync(string email);
        Task<string> ValidateConfirmationCodeAsync(string email, string code);
        Task ResetPassword(string resetToken, string newPassword);
    }
}
