using PhoneBook.Utilities;
using Spectre.Console;


namespace PhoneBook.Controllers;

class EmailController
{
    private readonly Validation _validation;

    public EmailController(Validation validation)
    {
        _validation = validation;
    }

    internal string? GetEmailSubject()
    {
        var emailSubject = AnsiConsole.Ask<string>("Please enter subject of email or enter 0 to return to main menu");
        emailSubject = _validation.CheckInputNullOrWhitespace(
            "Invalid entry, please try again",
            emailSubject);
        return emailSubject;
    }

    internal string? GetEmailBody()
    {
        var emailBody = AnsiConsole.Ask<string>("Please enter body of email or enter 0 to return to main menu");
        emailBody = _validation.CheckInputNullOrWhitespace(
            "Invalid entry, please try again",
            emailBody);
        return emailBody;
    }

}