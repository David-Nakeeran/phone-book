using PhoneBook.Models;
using PhoneBook.Utilities;
using Spectre.Console;


namespace PhoneBook.Controllers;

class CategoryController
{
    private readonly Validation _validation;

    public CategoryController(Validation validation)
    {
        _validation = validation;
    }

    internal string GetCategory(List<Category> categoryList)
    {
        AnsiConsole.MarkupLine("[bold]Pick category for contact[/]");
        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[green](Use arrow keys to navigate, then press enter)[/]");
        AnsiConsole.WriteLine();
        var userSelection = AnsiConsole.Prompt(
            new SelectionPrompt<String>()
                .Title("Select an option")
                .AddChoices(
                    categoryList.Select(c => c.CategoryName).ToList()
        ));
        return userSelection.ToString();
    }
}