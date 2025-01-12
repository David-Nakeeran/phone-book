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
        contactName = _validation.CheckInputNullOrWhitespace(string message, string ? input)
        // then return string
    }
}