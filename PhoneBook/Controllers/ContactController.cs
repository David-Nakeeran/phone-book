using PhoneBook.Utilities;
using Spectre.Console;


namespace PhoneBook.Controllers;

class ContactController
{
    private readonly Validation _validation;

    public ContactController(Validation validation)
    {
        _validation = validation;
    }

    internal string? GetContactName()
    {
        var contactName = AnsiConsole.Ask<string>("Please enter name of contact or enter 0 to return to main menu");
        contactName = _validation.ValidateString(
            "Please enter name of contact or enter 0 to return to main menu",
            "Invalid entry, please make sure to use only letters, press any key to continue...",
            contactName);
        return contactName;
    }

    internal string? GetContactEmail()
    {
        var emailAddress = AnsiConsole.Ask<string>("Please email address in format of 'example@example.com' or enter 0 to return to main menu");
        emailAddress = _validation.ValidateEmailAddress(
            "Please email address in format of 'example@example.com' or enter 0 to return to main menu",
            "Invalid email address, please enter in correct format: 'example@example.com'",
            emailAddress);
        return emailAddress;
    }
}