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
            try
            {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(_configuration.GetSection("EmailAddress").Value));
                email.To.Add(MailboxAddress.Parse(emailModel.ToEmail));
                email.Subject = emailModel.Subject;
                email.Body = new TextPart(TextFormat.Html)
                {
                    Text = emailModel.Body
                };

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(_configuration.GetSection("SmtpServer").Value, 587, SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync(_configuration.GetSection("EmailAddress").Value, _configuration.GetSection("EmailPassword").Value);
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
