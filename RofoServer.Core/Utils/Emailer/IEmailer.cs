using System.Threading.Tasks;

namespace RofoServer.Core.Utils.Emailer
{
    public interface IEmailer
    {
        public Task SendEmailAsync(string recipient, string subject, string message);
    }
}
