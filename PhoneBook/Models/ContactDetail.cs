using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PhoneBook.Models;

[Index(nameof(PhoneNumber), IsUnique = true)]
public class ContactDetail
{
    [Key]
    public int Id { get; set; }

    [Required]
    public required string Name { get; set; }

    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    [Required]
    [Phone]
    public required string PhoneNumber { get; set; }

    public int CategoryId { get; set; }

    [ForeignKey(nameof(CategoryId))]
    public Category Category { get; set; } // Category always assigned at runtime
}