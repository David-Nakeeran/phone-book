using Spectre.Console;
using PhoneBook.Enums;
using PhoneBook.Utilities;
namespace PhoneBook.Views;

class MenuHandler
{
    private readonly EnumHelper _enumHelper;

    public MenuHandler(EnumHelper enumHelper)
    {
        _enumHelper = enumHelper;
    }

    internal string ShowMenu()
    {
        Console.Clear();
        AnsiConsole.MarkupLine("[bold]Main Menu[/]");
        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[green](Use arrow keys to navigate, then press enter)[/]");
        AnsiConsole.WriteLine();
        var userSelection = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select an option")
                .AddChoices(
                    _enumHelper.GetDisplayName(MenuOptions.ViewAllRecords),
                    _enumHelper.GetDisplayName(MenuOptions.InsertRecord),
                    _enumHelper.GetDisplayName(MenuOptions.UpdateRecord),
                    _enumHelper.GetDisplayName(MenuOptions.DeleteRecord),
                    _enumHelper.GetDisplayName(MenuOptions.SendEmail),
                    _enumHelper.GetDisplayName(MenuOptions.Quit)
                    ));
        return userSelection;
    }

    internal bool ReturnToMainMenu(string? returnToMenu)
    {
        if (returnToMenu == "0") return true;
        return false;
    }

    internal void WaitForUserInput()
    {
        AnsiConsole.WriteLine("Press any key to continue...");
        Console.ReadKey(true);
    }
}