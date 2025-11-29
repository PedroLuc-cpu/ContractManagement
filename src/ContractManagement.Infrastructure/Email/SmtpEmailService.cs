using ContractManagement.Domain.Interfaces.Services;
using System.Net;
using System.Net.Mail;

namespace ContractManagement.Infrastructure.Email
{
    public class SmtpEmailService : IEmailService
    {
        private readonly SmtpClient _smtpClient;
        private readonly string _email;
        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var message = new MailMessage(_email, to, subject, body)
            {
                IsBodyHtml = true
            };
            await _smtpClient.SendMailAsync(message);
        }

        public SmtpEmailService()
        {
            var host = "smtp.gmail.com";
            var port = 587;
            var user = "pedrolucas@bemasoft.com.br";
            var password = "baue vmoq avvt kbvl ";
            _email = "pedrolucas@bemasoft.com.br";

            _smtpClient = new SmtpClient(host, port)
            {
                Credentials = new NetworkCredential(user, password),
                EnableSsl = true
            };

        }
    }
}
