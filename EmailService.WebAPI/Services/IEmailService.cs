using EmailService.WebAPI.Models;

namespace EmailService.WebAPI.Services
{
    public interface IEmailService
    {
        public Task<bool> SendEmail(EmailModel emailModel);
    }
}
