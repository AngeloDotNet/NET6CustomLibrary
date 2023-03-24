namespace NET6CustomLibrary.MailKit.Services;

public interface IEmailClient : IEmailSender
{
    Task<bool> SendEmailAsync(string recipientEmail, string replyToEmail, string subject, string htmlMessage, CancellationToken token = default);
}