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

    internal string GetContactName(string message)
    {
        var contactName = AnsiConsole.Ask<string>(message);
        contactName = _validation.ValidateString(
            message,
            "Invalid entry, please try again",
            contactName);
        return contactName;
    }

    internal string GetContactEmail(string message)
    {
        var emailAddress = AnsiConsole.Ask<string>(message);
        emailAddress = _validation.ValidateEmailAddress(
            message,
            "Invalid email address, please try again",
            emailAddress);
        return emailAddress;
    }

    internal string GetContactMobileNumber(string message)
    {
        var mobileNumber = AnsiConsole.Ask<string>(message);
        mobileNumber = _validation.ValidateMobileNumber(message, mobileNumber);
        return mobileNumber;
    }

    internal int GetContactId(string message)
    {
        var id = AnsiConsole.Ask<string>(message);
        int contactId = _validation.ValidateContactId(message, id);
        return contactId;
    }

    internal List<string> GetContactUpdateFields()
    {
        var fields = AnsiConsole.Prompt(
            new MultiSelectionPrompt<string>()
            .Title("Select which contact fields you wish to update")
            .AddChoices(new[] { "Name", "Email", "Mobile Number", "Category" })
        );
        return fields;
    }
}