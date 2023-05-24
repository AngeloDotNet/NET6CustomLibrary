namespace NET6CustomLibrary.MailKit.Services;

public class MailKitEmailSender : IEmailClient
{
    private readonly ILoggerService logger;
    private readonly IOptionsMonitor<SmtpOptions> smtpOptionsMonitor;

    public MailKitEmailSender(ILoggerService logger, IOptionsMonitor<SmtpOptions> smtpOptionsMonitor)
    {
        this.logger = logger;
        this.smtpOptionsMonitor = smtpOptionsMonitor;
    }

    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        return SendEmailAsync(email, string.Empty, subject, htmlMessage);
    }

    public async Task<bool> SendEmailAsync(string recipientEmail, string replyToEmail, string subject, string htmlMessage, CancellationToken token = default)
    {
        try
        {
            var options = smtpOptionsMonitor.CurrentValue;

            using SmtpClient client = new();

            await client.ConnectAsync(options.Host, options.Port, options.Security, token);

            if (!string.IsNullOrEmpty(options.Username))
            {
                await client.AuthenticateAsync(options.Username, options.Password, token);
            }

            MimeMessage message = new();

            message.From.Add(MailboxAddress.Parse(options.Sender));
            message.To.Add(MailboxAddress.Parse(recipientEmail));

            if (replyToEmail is not (null or ""))
            {
                message.ReplyTo.Add(MailboxAddress.Parse(replyToEmail));
            }

            message.Subject = subject;
            message.Body = new TextPart("html")
            {
                Text = htmlMessage
            };

            await client.SendAsync(message, token);
            await client.DisconnectAsync(true, token);

            logger.SaveLogInformation($"Message successfully sent to the email address {recipientEmail}");
            return true;
        }
        catch
        {
            logger.SaveLogError($"Couldn't send email to {recipientEmail} with message {htmlMessage}");
            return false;
        }
    }
}