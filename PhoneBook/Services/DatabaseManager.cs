using PhoneBook.Models;
using PhoneBook.Data;

namespace PhoneBook.Services;

class DatabaseManager
{
    private readonly ApplicationDbContext _applicationDbContext;

    public DatabaseManager(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    internal void CreateNewContact(string? name, string? email, string? phoneNumber)
    {
        _applicationDbContext.Add(new ContactDetail
        {
            Name = name,
            Email = email,
            PhoneNumber = phoneNumber
        });
        _applicationDbContext.SaveChanges();
    }

    internal List<ContactDetail> GetContactList()
    {
        return _applicationDbContext.ContactDetails.ToList();
    }

    internal void DeleteAContact(ContactDetail contactDetail)
    {

        _applicationDbContext.Remove(contactDetail);
        _applicationDbContext.SaveChanges();
    }

    internal ContactDetail? GetContactById(int contactId)
    {
        return _applicationDbContext.ContactDetails.SingleOrDefault(x => x.Id == contactId);
    }
}