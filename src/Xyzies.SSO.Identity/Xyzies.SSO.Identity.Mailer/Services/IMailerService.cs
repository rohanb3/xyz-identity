using SendGrid;
using System.Threading.Tasks;
using Xyzies.SSO.Identity.Mailer.Models;

namespace Xyzies.SSO.Identity.Mailer.Services
{
    public interface IMailerService
    {
        Task<Response> SendMail(MailSendingModel model);
    }
}
