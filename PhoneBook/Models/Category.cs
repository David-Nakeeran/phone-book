using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneBook.Models;

public class Category
{
    [Key]
    public int CategoryId { get; set; }

    [Required]
    public required string CategoryName { get; set; }

    public List<ContactDetail> Contacts { get; set; } = new List<ContactDetail>();
    // navigation property is a collection of all ContactDetail records that have the matching CategoryId
}