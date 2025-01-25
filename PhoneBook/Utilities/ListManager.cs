using PhoneBook.Models;
using Spectre.Console;

namespace PhoneBook.Utilities;

class ListManager
{
    internal bool IsListEmpty<T>(List<T> list) => list.Any();

    internal void PrintContacts(List<ContactDetailDTO> list)
    {
        var table = new Table();

        table.Title($"Contacts");

        table.AddColumn("ID");
        table.AddColumn("Name");
        table.AddColumn("Email");
        table.AddColumn("Mobile Number");
        table.AddColumn("Category");

        foreach (var contact in list)
        {

            table.AddRow(
            contact.DisplayId.ToString(),
            contact.Name.ToString(),
            contact.Email.ToString(),
            contact.PhoneNumber.ToString(),
            contact.CategoryName.ToString()
            );
        }

        AnsiConsole.Write(table);
    }

    internal void PrintAContact(ContactDetail obj)
    {
        var table = new Table();

        table.Title($"Contacts");
        table.AddColumn("Name");
        table.AddColumn("Email");
        table.AddColumn("Mobile Number");
        table.AddColumn("Category");

        table.AddRow(
        obj.Name.ToString(),
        obj.Email.ToString(),
        obj.PhoneNumber.ToString(),
        obj.Category.CategoryName.ToString()
        );
        AnsiConsole.Write(table);
    }

    internal string? FindMatchingContactPhoneNumber(List<ContactDetailDTO> contacts, int contactId)
    {
        foreach (var contact in contacts)
        {
            if (contact.DisplayId == contactId)
            {
                return contact.PhoneNumber;
            }
        }
        return null;
    }
}