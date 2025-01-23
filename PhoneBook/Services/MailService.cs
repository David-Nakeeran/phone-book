using MailKit.Net.Smtp;
using MimeKit;
using PhoneBook.Models;
using Microsoft.Extensions.Options;
using MailKit.Security;

namespace PhoneBook.Services;

public class MailService
{
    private readonly MailSettings _mailSettings;

    public MailService(IOptions<MailSettings> mailSettings)
    {
        _mailSettings = mailSettings.Value;
    }

    internal void SendMail(EmailDetail emailDetail)
    {
        var email = new MimeMessage();

        email.From.Add(new MailboxAddress($"{_mailSettings.Name}", $"{_mailSettings.EmailId}"));
        email.To.Add(new MailboxAddress($"{emailDetail.RecipientName}", $"{emailDetail.RecipientEmail}"));

        email.Subject = $"{emailDetail.Subject}";
        email.Body = new TextPart(MimeKit.Text.TextFormat.Plain)
        {
            Text = $"{emailDetail.Body}"
        };

        using (var smtp = new SmtpClient())
        {
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.UserName, _mailSettings.Password);

            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}