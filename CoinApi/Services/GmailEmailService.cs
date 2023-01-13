using CoinApi.Response_Models;
using Microsoft.Extensions.Options;
using System.Net.Mail;

namespace CoinApi.Services
{
    public class GmailEmailService : SmtpClient
    {

        public GmailEmailService(IOptions<EmailSettings> emailSettings) :
            base(emailSettings.Value.MailServer, emailSettings.Value.MailPort)
        {
            this.EnableSsl = true;
            this.UseDefaultCredentials = false;
            this.Credentials = new System.Net.NetworkCredential(emailSettings.Value.Sender, emailSettings.Value.Password);
        }

    }
}
