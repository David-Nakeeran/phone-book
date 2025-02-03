using System.ComponentModel.DataAnnotations;

namespace PhoneBook.Enums;

internal enum MenuOptions
{
    [Display(Name = "View All Records")]
    ViewAllRecords,

    [Display(Name = "Insert Record")]
    InsertRecord,

    [Display(Name = "Update Record")]
    UpdateRecord,

    [Display(Name = "Delete Record")]
    DeleteRecord,

    [Display(Name = "Send Email")]
    SendEmail,

    [Display(Name = "Quit")]
    Quit
}