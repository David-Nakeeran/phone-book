using Microsoft.Extensions.Logging;
using Spectre.Console;
using System.Net.Mail;

namespace PhoneBook.Utilities;

class Validation
{
    private readonly ILogger<Validation> _logger;

    public Validation(ILogger<Validation> logger)
    {
        _logger = logger;
    }
    internal string CheckInputNullOrWhitespace(string message, string? input)
    {
        while (string.IsNullOrWhiteSpace(input))
        {
            input = AnsiConsole.Ask<string>(message);
        }
        return input;
    }

    internal bool IsCharZero(string input)
    {
        if (input.Length == 1 && input.Equals("0")) return true;
        return false;
    }

    internal bool IsCharValid(string input)
    {
        return input.All(Char.IsLetter);
    }

    internal string? ValidateString(string message, string invalidMessage, string? input)
    {
        input = CheckInputNullOrWhitespace(message, input);
        if (IsCharZero(input)) return input;
        if (IsCharValid(input)) return input;
        while (!IsCharValid(input))
        {
            AnsiConsole.WriteLine(invalidMessage);
            Console.ReadKey(true);
            input = AnsiConsole.Ask<string>(message);
            input = CheckInputNullOrWhitespace(message, input);
            if (IsCharZero(input)) return input;
        }
        return input;
    }

    internal bool IsEmailValid(string emailAddress)
    {
        try
        {
            var email = new MailAddress(emailAddress);
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error occurred while validating email address");
            return false;
        }
    }

    internal string? ValidateEmailAddress(string message, string invalidMessage, string emailAddress)
    {
        emailAddress = CheckInputNullOrWhitespace(message, emailAddress);
        if (IsCharZero(emailAddress)) return emailAddress;
        if (IsEmailValid(emailAddress)) return emailAddress;
        while (!IsEmailValid(emailAddress))
        {
            AnsiConsole.WriteLine(invalidMessage);
            Console.ReadKey(true);
            emailAddress = AnsiConsole.Ask<string>(message);
            emailAddress = CheckInputNullOrWhitespace(message, emailAddress);
            if (IsCharZero(emailAddress)) return emailAddress;
        }
        return emailAddress;
    }
}