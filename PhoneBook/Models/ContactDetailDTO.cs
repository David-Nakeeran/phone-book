

namespace PhoneBook.Models;

public class ContactDetailDTO
{
    public int DisplayId { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string PhoneNumber { get; set; }
    public required string CategoryName { get; set; }
}