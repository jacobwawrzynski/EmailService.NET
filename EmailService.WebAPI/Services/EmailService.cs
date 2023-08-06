using EmailService.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;
using MailKit.Security;
using MailKit.Net.Smtp;

namespace EmailService.WebAPI.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> SendEmail(EmailModel emailModel)
        {
            var smtpHost = _configuration.GetSection("Host").Value;
            var emailAddress = _configuration.GetSection("EmailAddress").Value;
            var emailPassword = _configuration.GetSection("EmailPassword").Value;
            var port = Convert.ToInt32(_configuration.GetSection("Port").Value);

            try
            {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(emailAddress));
                email.To.Add(MailboxAddress.Parse(emailModel.ToEmail));
                email.Subject = emailModel.Subject;
                email.Body = new TextPart(TextFormat.Plain)
                {
                    Text = emailModel.Body
                };

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(smtpHost, port, SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync(emailAddress, emailPassword);
                    await client.SendAsync(email);
                    await client.DisconnectAsync(true);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }
    }
}
