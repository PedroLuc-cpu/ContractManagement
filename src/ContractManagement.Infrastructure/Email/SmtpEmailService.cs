using ContractManagement.Domain.Interfaces.Services;
using ContractManagement.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace ContractManagement.Infrastructure.Email
{
    public class SmtpEmailService : IEmailService, IEmailSender<ApplicationUser>
    {
        private readonly SmtpClient _smtpClient;
        private readonly string _email;

        public SmtpEmailService(IOptions<SmtpEmailOptions> options)
        {
            var smtpOptions = options.Value;
            _email = smtpOptions.FromEmail;
            _smtpClient = new SmtpClient(smtpOptions.Host, smtpOptions.Port)
            {
                Credentials = new NetworkCredential(smtpOptions.User, smtpOptions.Password),
                EnableSsl = smtpOptions.EnableSsl
            };
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var message = new MailMessage(_email, to, subject, body)
            {
                IsBodyHtml = true
            };
            await _smtpClient.SendMailAsync(message);
        }

        public async Task SendConfirmationLinkAsync(ApplicationUser user, string email, string confirmationLink)
        {
            await SendEmailAsync(email, "Confirm your email", $"Please confirm your account by clicking this link: <a href='{confirmationLink}'>link</a>");

        }

        public async Task SendPasswordResetLinkAsync(ApplicationUser user, string email, string resetLink)
        {
            await SendEmailAsync(email, "Reset your password", $"You can reset your password by clicking this link: <a href='{resetLink}'>link</a>");

        }

        public async Task SendPasswordResetCodeAsync(ApplicationUser user, string email, string resetCode)
        {
            await SendEmailAsync(email, "Reset your password", $"You can reset your password using this code: {resetCode}");
        }

        public async Task SendEmailAsync(string to, string subject, string body, byte[] attachmentData, string attachmentName)
        {
            var message = new MailMessage(_email, to, subject, body)
            {
                IsBodyHtml = true
            };

            await _smtpClient.SendMailAsync(message);
        }

    }
}
