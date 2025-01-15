using PhoneBook.Controllers;
using PhoneBook.Enums;
using PhoneBook.Services;
using PhoneBook.Views;

namespace PhoneBook.Coordinators;

class AppCoordinator
{
    private readonly MenuHandler _menuHandler;
    private readonly ContactController _contactController;
    private readonly DatabaseManager _databaseManager;

    public AppCoordinator(MenuHandler menuHandler, ContactController contactController, DatabaseManager databaseManager)
    {
        _menuHandler = menuHandler;
        _contactController = contactController;
        _databaseManager = databaseManager;
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
        _menuHandler.ReturnToMainMenu(contactName);
        var contactEmailAddress = _contactController.GetContactEmail();
        _menuHandler.ReturnToMainMenu(contactEmailAddress);
        var mobileNumber = _contactController.GetContactMobileNumber();
        _menuHandler.ReturnToMainMenu(mobileNumber);

        _databaseManager.CreateNewContact(contactName, contactEmailAddress, mobileNumber);
    }
}