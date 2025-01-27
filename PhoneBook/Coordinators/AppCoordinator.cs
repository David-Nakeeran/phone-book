using Spectre.Console;
using PhoneBook.Controllers;
using PhoneBook.Enums;
using PhoneBook.Services;
using PhoneBook.Views;
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
    private readonly EmailController _emailController;
    private readonly MailService _mailService;
    private readonly CategoryController _categoryController;


    public AppCoordinator(MenuHandler menuHandler, ContactController contactController, DatabaseManager databaseManager, ContactMapper contactMapper, ListManager listManager, EmailController emailController, MailService mailService, CategoryController categoryController)
    {
        _menuHandler = menuHandler;
        _contactController = contactController;
        _databaseManager = databaseManager;
        _contactMapper = contactMapper;
        _listManager = listManager;
        _emailController = emailController;
        _mailService = mailService;
        _categoryController = categoryController;

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
                case MenuOptions.SendEmail:
                    SendEmail();
                    break;
                case MenuOptions.Quit:
                    isAppActive = false;
                    break;
            }
        }
    }

    internal void AddContact()
    {
        var contactName = _contactController.GetContactName("Please enter name of contact or enter 0 to return to main menu");
        if (_menuHandler.ReturnToMainMenu(contactName)) return;
        var contactEmailAddress = _contactController.GetContactEmail("Please email address in format of 'example@example.com' or enter 0 to return to main menu");
        if (_menuHandler.ReturnToMainMenu(contactEmailAddress)) return;
        var mobileNumber = _contactController.GetContactMobileNumber("Please mobile number beginning with '07' and is 11 digits long or enter 0 to return to main menu");
        if (_menuHandler.ReturnToMainMenu(mobileNumber)) return;

        var categoryList = _databaseManager.GetCategoryList();
        var selectedCategory = _categoryController.GetCategory(categoryList);
        var categoryId = categoryList.First(c => c.CategoryName == selectedCategory).CategoryId;

        _databaseManager.CreateNewContact(contactName, contactEmailAddress, mobileNumber, categoryId);
    }

    internal List<ContactDetailDTO> GetAllContacts()
    {
        var contactList = _databaseManager.GetContactList();
        var contactListDTO = _contactMapper.MapContactsToDTO(contactList);
        if (!_listManager.IsListEmpty(contactListDTO))
        {
            AnsiConsole.WriteLine("No contacts available, returning to main menu...");
            return new List<ContactDetailDTO>();
        }
        return contactListDTO;
    }

    internal void ViewAllContacts()
    {
        var contactListDTO = GetAllContacts();
        _listManager.PrintContacts(contactListDTO);
        _menuHandler.WaitForUserInput();
    }

    internal void DeleteContact()
    {
        ViewAllContacts();
        var contactId = _contactController.GetContactId("Please enter id of contact you would like to delete, or enter 0 to return to main menu");
        if (_menuHandler.ReturnToMainMenu(contactId.ToString())) return;
        var contactList = GetAllContacts();
        var contactPhoneNumber = _listManager.FindMatchingContactPhoneNumber(contactList, contactId);
        var contact = _databaseManager.GetContactByPhoneNumber(contactPhoneNumber);
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

    internal ContactDetail? GetContactObject(int contactId)
    {
        var contactList = GetAllContacts();
        var contactPhoneNumber = _listManager.FindMatchingContactPhoneNumber(contactList, contactId);
        if (contactPhoneNumber == null)
        {
            AnsiConsole.WriteLine("No matching Id found, returning to main menu");
            return null;
        }
        return _databaseManager.GetContactByPhoneNumber(contactPhoneNumber);
    }

    internal void UpdateContactFields(ContactDetail contact, List<string> fields)
    {
        foreach (var field in fields)
        {
            switch (field)
            {
                case "Name":
                    contact.Name = _contactController.GetContactName("Please update name of contact");
                    break;
                case "Email":
                    contact.Email = _contactController.GetContactEmail("Please update email in format of 'example@example.com'");
                    break;
                case "Mobile Number":
                    contact.PhoneNumber = _contactController.GetContactMobileNumber("Please update mobile number beginning with '07' and is 11 digits long");
                    break;
                case "Category":
                    var categoryList = _databaseManager.GetCategoryList();
                    var selectedCategory = _categoryController.GetCategory(categoryList);
                    var newCategory = categoryList.First(c => c.CategoryName == selectedCategory);
                    contact.Category = newCategory;
                    break;
            }
        }
        _databaseManager.UpdateContact(contact);
    }

    internal void UpdateContact()
    {
        ViewAllContacts();
        var contactId = _contactController.GetContactId("Please enter id of contact you would like to update, or enter 0 to return to main menu");
        if (_menuHandler.ReturnToMainMenu(contactId.ToString())) return;

        var contact = GetContactObject(contactId);
        if (contact != null)
        {
            _listManager.PrintAContact(contact);

            var fieldsToBeUpdated = _contactController.GetContactUpdateFields();
            UpdateContactFields(contact, fieldsToBeUpdated);
        }
        return;
    }

    internal void SendEmail()
    {
        ViewAllContacts();
        var contactId = _contactController.GetContactId("Please enter id of contact you would like to send an email to, or enter 0 to return to main menu");
        if (_menuHandler.ReturnToMainMenu(contactId.ToString())) return;
        var contact = GetContactObject(contactId);

        if (contact != null)
        {
            var emailSubject = _emailController.GetEmailSubject();
            var emailBody = _emailController.GetEmailBody();

            var email = new EmailDetail
            {
                RecipientName = contact.Name,
                RecipientEmail = contact.Email,
                Subject = emailSubject,
                Body = emailBody
            };
            _mailService.SendMail(email);
            AnsiConsole.WriteLine("email have been sent");
            return;
        }
        AnsiConsole.WriteLine("Email could not be sent, returning to main menu");
        return;
    }
}