namespace CoinApi.Services.EmailService
{
    public interface IEmailService
    {
        string GetPasswordResetLink(string verificationCode);
        Task<bool> SendPasswordResetEmail(string destinationEmail, string passwordResetLink,string name);
    }
}
