using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using PhoneBook.Models;
using Microsoft.Extensions.Options;

namespace PhoneBook.Services;

class MailService
{
    private readonly IOptions<MailSettings> _mailSettings;

    public MailService(IOptions<MailSettings> mailSettings)
    {
        _mailSettings = mailSettings;
    }

    internal void SendMail()
    {

    }
}