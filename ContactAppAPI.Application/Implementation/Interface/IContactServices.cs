using ContactAppAPI.Application.DTO;
using ContactAppAPI.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactAppAPI.Application.Implementation.Interface
{
    public interface IContactServices
    {        
        Task<ContactUser> GetContactByIdAsync(string id);
        Task<IEnumerable<ContactUser>> GetAllContactsAsync(int page, int pageSizes);
        Task<string> AddNewUserAsync(ContactUserDto contactUserDto);
        Task<ContactUser> UpdateUserAsync(string Id, [FromBody] ContactUserDto contact);
        Task<ContactUser> GetSingleContactByEmail(string email);
        Task<ContactUser> GetSingleContactByNumber(string number);
        Task<IEnumerable<ContactUser>> DeleteAllContact();
        Task<string> DeleteSingleContactByIdAsync(string id);
        Task<string> AddImage(ContactUser model);
    }
}
