using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xyzies.SSO.Identity.Data.Entity;
using Xyzies.SSO.Identity.Data.Helpers;
using Xyzies.SSO.Identity.Data.Repository;
using Xyzies.SSO.Identity.Mailer.Services;

namespace Xyzies.SSO.Identity.Services.Service.ResetPassword
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class ResetPasswordService : IResetPasswordService
    {
        private readonly IMailerService _mailerService;
        private readonly IUserService _userService;
        private readonly IPasswordResetRequestRepository _passwordResetRequestRepository;
        private readonly IConfigurationRepository _configurationRepository;

        public ResetPasswordService(IMailerService mailerService,
            IPasswordResetRequestRepository passwordResetRequestRepository,
            IUserService userService,
            IConfigurationRepository configurationRepository)
        {
            _mailerService = mailerService ?? throw new ArgumentNullException(nameof(mailerService));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _passwordResetRequestRepository = passwordResetRequestRepository ?? throw new ArgumentNullException(nameof(passwordResetRequestRepository));
            _configurationRepository = configurationRepository ?? throw new ArgumentNullException(nameof(configurationRepository));
        }

        public async Task ResetPassword(string resetToken, string newPassword)
        {
            try
            {
                Guid.TryParse(resetToken, out Guid requestId);

                var request = (await _passwordResetRequestRepository.GetByAsync(r => r.Id == requestId)) ?? throw new KeyNotFoundException();

                await _userService.UpdateUserPasswordAsync(request.Email, newPassword);
                await _passwordResetRequestRepository.RemoveAsync(requestId);
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (ArgumentException)
            {
                throw;
            }
        }

        public async Task SendConfirmationCodeAsync(string email)
        {
            try
            {
                var code = GenerateFourDigitCode();

                await _passwordResetRequestRepository.AddAsync(new PasswordResetRequest
                {
                    Id = Guid.NewGuid(),
                    Email = email,
                    Code = code
                });

                var htmlTemplate =
                    (await _configurationRepository.GetByAsync(config => config.Id == Consts.ConfigurationKeys.HTMLCodeEmailTemplate))?.Value
                    ?? throw new ArgumentException("Missing html template for reset code mail");
                var textTemplate =
                    (await _configurationRepository.GetByAsync(config => config.Id == Consts.ConfigurationKeys.TextCodeEmailTemplate))?.Value
                    ?? throw new ArgumentException("Missing text template for reset code mail");

                var response = await _mailerService.SendMail(new Mailer.Models.MailSendingModel
                {
                    From = new SendGrid.Helpers.Mail.EmailAddress("xyzies.support@xyzies.com"),
                    ReplyTo = new SendGrid.Helpers.Mail.EmailAddress(email),
                    PlainTextContent = FillTemplateWithValues(textTemplate, code, email),
                    HtmlContent = FillTemplateWithValues(htmlTemplate, code, email),
                    Subject = "Verify your account"
                });

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<string> ValidateConfirmationCodeAsync(string email, string code)
        {
            try
            {
                var resetHash = (await _passwordResetRequestRepository.GetByAsync(request => request.Email == email && request.Code == code))?.Id;

                return resetHash?.ToString() ?? throw new KeyNotFoundException();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private string GenerateFourDigitCode()
        {
            var random = new Random();
            return random.Next(0, 9999).ToString().PadLeft(4, '0');
        }

        private string FillTemplateWithValues(string template, string codeValue, string emailValue)
        {
            return template.Replace("{code}", codeValue).Replace("{email}", emailValue);
        }
    }
}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
