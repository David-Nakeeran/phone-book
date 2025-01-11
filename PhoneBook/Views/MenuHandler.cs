using Spectre.Console;
using PhoneBook.Enums;
namespace PhoneBook.Views;

class MenuHandler
{
    internal MenuOptions ShowMenu()
    {
        Console.Clear();
        AnsiConsole.MarkupLine("[bold]Main Menu[/]");
        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[green](Use arrow keys to navigate, then press enter)[/]");
        AnsiConsole.WriteLine();
        var userSelection = AnsiConsole.Prompt(
            new SelectionPrompt<MenuOptions>()
                .Title("Select an option")
                .AddChoices(
                    MenuOptions.ViewAllRecords,
                    MenuOptions.InsertRecord,
                    MenuOptions.UpdateRecord,
                    MenuOptions.DeleteRecord,
                    MenuOptions.Quit
                    ));
        return userSelection;
    }
}