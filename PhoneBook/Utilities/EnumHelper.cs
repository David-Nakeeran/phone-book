using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace PhoneBook.Utilities;

class EnumHelper
{
    internal string GetDisplayName(Enum enumValue)
    {
        return enumValue.GetType()
            .GetMember(enumValue.ToString())
            .FirstOrDefault()?
            .GetCustomAttribute<DisplayAttribute>()?
            .Name ?? enumValue.ToString();
    }
}