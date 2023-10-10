using ContactAppAPI.Application.DTO;
using ContactAppAPI.Domain.Model;

namespace ContactAppAPI.Application.Implementation.Interface
{
    public interface IContactDtoMapper 
    {
        public List<ContactUserDto> MapContactsToDTOs(List<ContactUser> contacts);
        public ContactUserDto MapContactToContactDTO(ContactUser contact);

    }
}
