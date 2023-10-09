using ContactAppAPI.Application.DTO;
using ContactAppAPI.Domain.Model;
using ContactAppAPI.Persistence.DataContext;
using Microsoft.AspNetCore.Mvc;

namespace ContactAppAPI.Persistence.Repository.Interface
{
    public interface IContactRepository
    {
        Task<ContactUser> GetSingleContactById(string id);
        Task<IEnumerable<ContactUser>> GetAllContactsAsync(int page, int pageSize);
        Task<ContactUser> DeleteSingleContactByIdAsync(int id);
        Task<IEnumerable<ContactUser>> DeleteAllContact();
        Task<ContactUser> AddNewUserAsync(ContactUserDto contactUserDto);
        Task<ContactUser> UpdateUserAsync(int Id, [FromBody] ContactUserDto contactUserDto);
        Task<ContactUser> GetSingleContactByNumber(string number);

        Task<ContactUser> GetSingleContactByEmail(string email);

    }
}
