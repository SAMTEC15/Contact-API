using ContactAppAPI.Application.DTO;
using ContactAppAPI.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactAppAPI.Persistence.Repository
{
    public interface IContactRepository
    {
        Task<ContactUser> GetSingleContactById(int id);
        Task<IEnumerable<ContactUser>> GetAllContactsAsync();
        Task<ContactUser> DeleteSingleContactByIdAsync(int id);
        Task<IEnumerable<ContactUser>> DeleteAllContact();
        Task<ContactUser> AddNewUserAsync(ContactUser contactUser);
        Task<ContactUser> UpdateUserAsync(int Id, [FromBody] ContactUserDto contact);
    }
}
