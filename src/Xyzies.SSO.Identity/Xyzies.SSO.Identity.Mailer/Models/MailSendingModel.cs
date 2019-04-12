using SendGrid.Helpers.Mail;

namespace Xyzies.SSO.Identity.Mailer.Models
{
    public class MailSendingModel
    {
        public EmailAddress From { get; set; }
        public EmailAddress ReplyTo { get; set; }
        public string Subject { get; set; }
        public string PlainTextContent { get; set; }
        public string HtmlContent { get; set; }
    }
}
