using AutoMapper;
using PhoneBook.Models;

namespace PhoneBook.Mappers;

class ContactMapper
{
    private readonly IMapper _mapper;

    public ContactMapper(IMapper mapper)
    {
        _mapper = mapper;
    }

    internal List<ContactDetailDTO> MapContactsToDTO(List<ContactDetail> contactList)
    {
        var contactDetailDTOs = contactList.Select((contact, index) =>
        {
            var dto = _mapper.Map<ContactDetailDTO>(contact);
            dto.DisplayId = index + 1;
            dto.CategoryName = contact.Category.CategoryName;
            return dto;
        }).ToList();
        return contactDetailDTOs;
    }
}