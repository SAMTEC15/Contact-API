using ContactAppAPI.Application.DTO;
using ContactAppAPI.Domain.Model;
using ContactAppAPI.Persistence.DataContext;
using ContactAppAPI.Persistence.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ContactAppAPI.Application.Implementation.Services
{
    public class ContactController
    {
        private readonly IContactRepository _contactRepository;

        public ContactController(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task<IActionResult> AddNewUser(ContactUserDto contactUserDto)
        {
            // Validate the user input
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }

            // Add the new user to the database
            var result = await _contactRepository.AddNewUser(contactUserDto);

            // Return the result
            return result;
        }


        public async Task<ContactUser> GetContactByIdAsync(int id)
        {
            var contact = await _contactRepository.GetSingleContactById(id);
            if(contact == null)
            {
                ModelState.AddModelError("name", "Can not have same DisplayOrder Name.");
            }

            return contact;
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

    }
}
