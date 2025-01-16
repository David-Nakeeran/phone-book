using AutoMapper;
using PhoneBook.Models;

namespace PhoneBook.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ContactDetail, ContactDetailDTO>();
    }
}