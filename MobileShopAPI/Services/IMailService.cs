using SendGrid.Helpers.Mail;
using SendGrid;

namespace MobileShopAPI.Services
{
    public interface IMailService
    {
        Task SendEmailAsync(string toEmail, string subject, string body);

    }

    public class SendGridMailService:IMailService
    {
        private IConfiguration _configuration;
        public SendGridMailService(IConfiguration configuration)
        {
            _configuration  = configuration;
        }
        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var apiKey = _configuration["SendGridAPIKey"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("anhkhoa3301@gmail.com", "MobileShop");
            var to = new EmailAddress(toEmail);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, body, body);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
