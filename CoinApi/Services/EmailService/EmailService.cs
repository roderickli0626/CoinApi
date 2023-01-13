using CoinApi.Response_Models;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Text.Encodings.Web;


namespace CoinApi.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly IOptions<EmailSettings> _emailSettings;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        public EmailService(IOptions<EmailSettings> emailSettings, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _emailSettings = emailSettings;
            _hostingEnvironment = hostingEnvironment;
        }
        public string GetPasswordResetLink(string verificationCode)
        {
            var passwordResetBaseUrl = _emailSettings.Value.PasswordResetBaseUrl.TrimEnd(char.Parse("/")) + "/";
            return $"{passwordResetBaseUrl}?code={verificationCode}";
        }
        public async Task<bool> SendPasswordResetEmail(string destinationEmail, string passwordResetLink, string name)
        {
            try
            {
                using (var mailClient = new GmailEmailService(_emailSettings))
                {
                    MailMessage email = new MailMessage(new MailAddress(_emailSettings.Value.Sender, _emailSettings.Value.SenderName),
                        new MailAddress(destinationEmail));
                    string filepath = _hostingEnvironment.WebRootPath + "/EmailTemplates/_SendResetPasswordEmail.html";
                    var emailTemplate = System.IO.File.ReadAllText(filepath);
                    StreamReader str = new StreamReader(filepath);
                    string mailBody = str.ReadToEnd();
                    str.Close();
                    mailBody = mailBody.Replace("##imageurl##", _emailSettings.Value.ImageUrl.ToString());
                    mailBody = mailBody.Replace("##redirecturl##", passwordResetLink);
                    mailBody = mailBody.Replace("##username##", name);
                    mailBody = mailBody.Replace("##year##", DateTime.Now.Year.ToString());

                    email.Subject = "Resetting your password";
                    email.Body = mailBody;
                    email.IsBodyHtml = true;

                    await mailClient.SendMailAsync(email);
                    return true;
                }
            }
            catch (SmtpException)
            {
                return false;
            }

            return false;
        }
    }
}
