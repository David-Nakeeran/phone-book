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
        return input.All(c => Char.IsLetter(c) || c == ' ');
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

    internal int CheckStringLength(string input)
    {
        return input.Length;
    }

    internal bool IsStringValidNumbers(string input)
    {
        if (int.TryParse(input, out _)) return true;
        return false;
    }

    internal string ValidateMobileNumberLength(string input)
    {
        int inputLength = CheckStringLength(input);
        if (inputLength == 11) return input;
        while (inputLength != 11)
        {
            if (inputLength < 11)
            {
                input = AnsiConsole.Ask<string>("Mobile number is too short, please try again");
                input = CheckInputNullOrWhitespace("Please make sure to enter valid mobile number", input);
                inputLength = CheckStringLength(input);
            }
            else if (inputLength > 11)
            {
                input = AnsiConsole.Ask<string>("Mobile number is too long, please try again");
                input = CheckInputNullOrWhitespace("Please make sure to enter valid mobile number", input);
                inputLength = CheckStringLength(input);
            }
        }
        return input;
    }

    internal string ValidateCorrectMobilePrefixUK(string input)
    {
        while (!input.StartsWith("07"))
        {
            input = AnsiConsole.Ask<string>("Make sure mobile number enter begins with '07', please try again");
            input = CheckInputNullOrWhitespace("Please make sure to enter valid mobile number", input);
        }
        return input;
    }

    internal string ValidateMobileNumber(string message, string input)
    {
        input = CheckInputNullOrWhitespace(message, input);
        if (IsCharZero(input)) return input;

        while (true)
        {
            input = ValidateMobileNumberLength(input);
            input = ValidateCorrectMobilePrefixUK(input);
            if (IsStringValidNumbers(input)) break;

            input = AnsiConsole.Ask<string>("Please make sure to only enter numbers");
            input = CheckInputNullOrWhitespace("Please make sure to enter valid mobile number", input);
        }
        return input;
    }
}