using PhoneBook.Enums;
using PhoneBook.Views;

namespace PhoneBook.Coordinators;

class AppCoordinator
{
    private readonly MenuHandler _menuHandler;

    public AppCoordinator(MenuHandler menuHandler)
    {
        _menuHandler = menuHandler;
    }
    internal void Start()
    {
        bool isAppActive = true;

        while (isAppActive)
        {
            var userSelection = _menuHandler.ShowMenu();
            switch (userSelection)
            {
                case MenuOptions.ViewAllRecords:
                    Console.WriteLine("View all records");
                    break;
                case MenuOptions.InsertRecord:
                    Console.WriteLine("Insert Record");
                    break;
                case MenuOptions.UpdateRecord:
                    Console.WriteLine("Update Record");
                    break;
                case MenuOptions.DeleteRecord:
                    Console.WriteLine("Delete record");
                    break;
                case MenuOptions.Quit:
                    Console.WriteLine("Quit app");
                    isAppActive = false;
                    break;
            }
        }
    }
}