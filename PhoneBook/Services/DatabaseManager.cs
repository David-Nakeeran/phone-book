using PhoneBook.Models;
using PhoneBook.Data;
using Microsoft.EntityFrameworkCore;

namespace PhoneBook.Services;

class DatabaseManager
{
    private readonly ApplicationDbContext _applicationDbContext;

    public DatabaseManager(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    internal void CreateNewContact(string name, string email, string phoneNumber, int categoryId)
    {
        _applicationDbContext.Add(new ContactDetail
        {
            Name = name,
            Email = email,
            PhoneNumber = phoneNumber,
            CategoryId = categoryId
        });
        _applicationDbContext.SaveChanges();
    }

    internal List<ContactDetail> GetContactList()
    {
        return _applicationDbContext.ContactDetails
            .Include(c => c.Category) // load related data
            .ToList();
    }

    internal List<Category> GetCategoryList()
    {
        return _applicationDbContext.Categories
            .Include(c => c.Contacts) // load related data
            .ToList();
    }

    internal void DeleteAContact(ContactDetail contactDetail)
    {

        _applicationDbContext.Remove(contactDetail);
        _applicationDbContext.SaveChanges();
    }

    internal ContactDetail? GetContactByPhoneNumber(string? phoneNumber)
    {
        return _applicationDbContext.ContactDetails
            .Include(c => c.Category)
            .SingleOrDefault(x => x.PhoneNumber == phoneNumber);
    }

    internal void UpdateContact(ContactDetail contact)
    {
        _applicationDbContext.ContactDetails.Update(contact);
        _applicationDbContext.SaveChanges();
    }
}