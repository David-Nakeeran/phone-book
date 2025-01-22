

namespace PhoneBook.Models;

public class MailSettings
{
    public string? Host { get; set; }
    public int Port { get; set; }
    public string? Name { get; set; }
    public string? EmailId { get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; }
    public bool UseStartTls { get; set; }
}

