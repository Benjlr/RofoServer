using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace RofoServer.Core.Utils.Emailer
{
    public class SendGridEmailer : IEmailer
    {
        private readonly IConfiguration _config;

        public SendGridEmailer(IConfiguration config) {
            _config = config;
        }
        public async Task SendEmailAsync(string recipient, string subject, string htmlMessage) =>
            await new SendGridClient(_config["SendGridAPI"])
                .SendEmailAsync(
                    MailHelper.CreateSingleEmail(
                        new EmailAddress(
                            _config["EmailSettings:FromAddress"],
                            _config["EmailSettings:FromEntity"]), 
                        new EmailAddress(recipient), 
                        subject, 
                        "", 
                        htmlMessage));
        
    }
}
