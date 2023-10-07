using ContactAppAPI.Application.DTO;
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
        Task<IActionResult> GetSingleContactById(int id);
        Task<IActionResult> GetAllContactAsync();
        Task<IActionResult> DeleteSingleContactByIdAsync(int id);
        Task<IActionResult> DeleteAllContact();
        Task<IActionResult> AddNewUser(ContactUserDto appUser);
        Task<IActionResult> UpdateUserAsync(int Id, [FromBody] ContactUserDto contact);
    }
}
