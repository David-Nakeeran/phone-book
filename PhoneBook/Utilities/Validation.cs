using Spectre.Console;

namespace PhoneBook.Utilities;

class Validation
{
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
        if (input.Length == 1 && input.Equals('0')) return true;
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
}