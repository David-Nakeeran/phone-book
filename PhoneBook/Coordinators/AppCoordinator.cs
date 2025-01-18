using Spectre.Console;
using PhoneBook.Controllers;
using PhoneBook.Enums;
using PhoneBook.Services;
using PhoneBook.Views;
using PhoneBook.Data;
using PhoneBook.Models;
using PhoneBook.Mappers;
using PhoneBook.Utilities;

namespace PhoneBook.Coordinators;

class AppCoordinator
{
    private readonly MenuHandler _menuHandler;
    private readonly ContactController _contactController;
    private readonly DatabaseManager _databaseManager;
    private readonly ContactMapper _contactMapper;
    private readonly ListManager _listManager;


    public AppCoordinator(MenuHandler menuHandler, ContactController contactController, DatabaseManager databaseManager, ContactMapper contactMapper, ListManager listManager)
    {
        _menuHandler = menuHandler;
        _contactController = contactController;
        _databaseManager = databaseManager;
        _contactMapper = contactMapper;
        _listManager = listManager;

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
                    ViewAllContacts();
                    break;
                case MenuOptions.InsertRecord:
                    AddContact();
                    break;
                case MenuOptions.UpdateRecord:
                    UpdateContact();
                    break;
                case MenuOptions.DeleteRecord:
                    DeleteContact();
                    break;
                case MenuOptions.Quit:
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

    internal void ViewAllContacts()
    {
        var contactList = _databaseManager.GetContactList();
        var contactListDTO = _contactMapper.MapContactsToDTO(contactList);
        if (!_listManager.IsListEmpty(contactListDTO))
        {
            AnsiConsole.WriteLine("No contacts available, returning to main menu...");
            return;
        }
        _listManager.PrintContacts(contactListDTO);
        _menuHandler.WaitForUserInput();
    }

    internal void DeleteContact()
    {
        ViewAllContacts();
        var contactId = _contactController.GetContactId("Please enter id of contact you would like to delete, or enter 0 to return to main menu");
        _menuHandler.ReturnToMainMenu(contactId.ToString());
        var contact = _databaseManager.GetContactById(contactId);
        if (contact != null)
        {
            _databaseManager.DeleteAContact(contact);
        }
        else
        {
            AnsiConsole.WriteLine("Contact not found, returning to main menu...");
            return;
        }
    }

    internal void UpdateContact()
    {
        ViewAllContacts();
        var contactId = _contactController.GetContactId("Please enter id of contact you would like to update, or enter 0 to return to main menu");
        _menuHandler.ReturnToMainMenu(contactId.ToString());
        var contact = _databaseManager.GetContactById(contactId);
        Console.WriteLine(contact);

        _contactController.GetContactUpdateFields();
    }
}