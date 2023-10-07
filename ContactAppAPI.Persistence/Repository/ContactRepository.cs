using ContactAppAPI.Application.DTO;
using ContactAppAPI.Domain.Model;
using ContactAppAPI.Persistence.DataContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactAppAPI.Persistence.Repository
{
    public class ContactRepository : IContactRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ContactUserDbContext _contactUserDbContext;
        private readonly UserManager<ContactUser> _userManager;

        public ContactRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<IActionResult> AddNewUser(ContactUserDto contactUserDto)
        {
            var user = await _contactUserDbContext.ContactUsers.FindAsync(contactUserDto.Email);
            if (user == null)
            {
                var data = new ContactUser()
                {
                    FirstName = contactUserDto.FirstName,
                    LastName = contactUserDto.LastName,
                    Email = contactUserDto.Email,
                    PhoneNumber = contactUserDto.PhoneNumber,
                    
                };

                var responds = await _userManager.CreateAsync(data); // method implementation takes only user info and password
                if (responds.Succeeded)
                {
                    await _userManager.AddToRoleAsync(data, "User");
                    return new OkObjectResult("User created successfully");
                }
                else
                {                    
                    return new BadRequestObjectResult($"User creation failed!");
                }
            }
            else
            {
                return new BadRequestObjectResult("User with this email already exists.");
            }          

        }

        public async Task<IActionResult> DeleteAllContact()
        {
            var contacts = await _contactUserDbContext.ContactUsers.ToListAsync();

            foreach (var contact in contacts)
            {
                _contactUserDbContext.Remove(contact);
            }
            await _contactUserDbContext.SaveChangesAsync();

            return new OkObjectResult("All Details from your database has been successfully deleted");
        }

        public async Task<IActionResult> DeleteSingleContactByIdAsync(int id)
        {
            var contact = await _contactUserDbContext.ContactUsers.FindAsync(id);
            if (contact == null)
            {
                return new BadRequestObjectResult("Not Found");
            }
            _contactUserDbContext.Remove(contact);
            await _contactUserDbContext.SaveChangesAsync();
            return new OkObjectResult("Successfully deleted");
        }

        public async Task<IActionResult> GetAllContactAsync()
        {
            var contacts = await _contactUserDbContext.ContactUsers.ToListAsync();
            return new OkObjectResult(contacts);
        }

        public async Task<IActionResult> GetSingleContactById(int id)
        {
            var check = await _contactUserDbContext.ContactUsers.FindAsync(id);
            if(check == null)
            {
                return new BadRequestObjectResult("Not found");
            }
            else
            {
                return new OkObjectResult(check);
            }
        }

        public async Task<IActionResult> UpdateUserAsync(int Id, [FromBody] ContactUserDto contact)
        {
            var contactToUpdate = await _contactUserDbContext.ContactUsers.FindAsync(Id);
            if (contactToUpdate == null)
            {
                return new BadRequestObjectResult("Not found");
            }
           var updated = _contactUserDbContext.ContactUsers.Update(contactToUpdate);
            return new OkObjectResult("Updated Successfully");
        }
    }
}
