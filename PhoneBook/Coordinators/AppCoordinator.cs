using PhoneBook.Controllers;
using PhoneBook.Enums;
using PhoneBook.Views;

namespace PhoneBook.Coordinators;

class AppCoordinator
{
    private readonly MenuHandler _menuHandler;
    private readonly ContactController _contactController;

    public AppCoordinator(MenuHandler menuHandler, ContactController contactController)
    {
        _menuHandler = menuHandler;
        _contactController = contactController;
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
                    AddContact();
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

    internal void AddContact()
    {
        var contactName = _contactController.GetContactName();
        if (contactName == "0") return;
        // var contactEmailAddress = _contactController.GetContactEmail();
        // if (contactName == "0") return;
        Console.WriteLine(contactName);
    }
}