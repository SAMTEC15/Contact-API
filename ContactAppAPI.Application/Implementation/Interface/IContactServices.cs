using ContactAppAPI.Application.DTO;
using ContactAppAPI.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactAppAPI.Application.Implementation.Interface
{
    public interface IContactServices
    {
        Task<ContactUser> AddNewUser(ContactUserDto contactUserDto);
        Task<ContactUser> GetContactByIdAsync(int id);
        Task<IEnumerable<ContactUser>> GetAllContactsAsync();
    }
}
