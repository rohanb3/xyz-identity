using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;
using Xyzies.SSO.Identity.Mailer.Helpers;
using Xyzies.SSO.Identity.Mailer.Models;

namespace Xyzies.SSO.Identity.Mailer.Services
{
    public class MailerService : IMailerService
    {
        private readonly SendGridClient _client;

        public MailerService(IOptionsMonitor<MailerOptions> mailerOptionsMonitor)
        {
            var options = mailerOptionsMonitor.CurrentValue ?? throw new ArgumentNullException(nameof(mailerOptionsMonitor));
            _client = new SendGridClient(options.ApiKey);
        }

        public async Task<Response> SendMail(MailSendingModel model)
        {
            var msg = MailHelper.CreateSingleEmail(model.From, model.ReplyTo, model.Subject, model.PlainTextContent, model.HtmlContent);
            var response = await _client.SendEmailAsync(msg);

            if(response.StatusCode != System.Net.HttpStatusCode.Accepted)
            {
                throw new ApplicationException(response.StatusCode.ToString());
            }

            return response;
        }
    }
}
