using ContactAppAPI.Application.DTO;
using ContactAppAPI.Domain.Model;
using ContactAppAPI.Persistence.DataContext;
using ContactAppAPI.Persistence.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ContactAppAPI.Application.Implementation.Services
{
    public class ContactServices
    {
        private readonly IContactRepository _contactRepository;
        private readonly UserManager<ContactUser> _userManager;

        public ContactServices(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task<ContactUser> AddNewUserAsync(ContactUserDto contactUserDto)
        {
            // Validate the user input
            if (string.IsNullOrEmpty(contactUserDto.FirstName) || string.IsNullOrEmpty(contactUserDto.LastName) || string.IsNullOrEmpty(contactUserDto.Email) || (!Regex.IsMatch(contactUserDto.Email, @"^[a-zA-Z0-9.!#$%&'*+-/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$")))
            {

                return null;
            }

            var user = await _userManager.FindByEmailAsync(contactUserDto.Email);
            if (user != null)
            {
                // Create a new user
                var contactUser = new ContactUser()
                {
                    FirstName = contactUserDto.FirstName,
                    LastName = contactUserDto.LastName,
                    Email = contactUserDto.Email,
                    PhoneNumber = contactUserDto.PhoneNumber,
                };
            }

            return user;

        }

        public async Task<ContactUser> GetContactByIdAsync(int id)
        {

            var check = await _contactRepository.GetSingleContactById(id);
            if (check != null)
            {
                return check;
            }
            else
            {
                return null;
            }

        }
        public async Task<IEnumerable<ContactUser>> GetAllContactsAsync()
        {
            var contacts = await _contactRepository.GetAllContactsAsync();

            return contacts;
        }

        public async Task<ContactUser> UpdateUserAsync(int Id, [FromBody] ContactUserDto contact)
        {
            // Validate the contact data.

            // Get the contact with the specified ID from the repository layer.
            var contactUser = await _contactRepository.GetSingleContactById(Id);
            if (contactUser == null)
            {
                return null;
            }

            contactUser.FirstName = contact.FirstName;
            contactUser.LastName = contact.LastName;
            contactUser.Email = contact.Email;
            contactUser.PhoneNumber = contact.PhoneNumber;

            // Save the updated contact to the repository layer.
            //var added = await _contactRepository.UpdateUserAsync(contactUser);

            return contactUser;
        }
        /*public Task<ContactUser> DeleteSingleContactByIdAsync(int id)
        {

        }
        public Task<IEnumerable<ContactUser>> DeleteAllContact()
        {

        }*/

        /*public async Task<ContactUser> CreateContactAsync(ContactUserDto contactUserDto)
        {
            // Validate the contact data.

            // Create a new contact object using the validated data.
            var contact = new ContactUser()
            {
                FirstName = contactUserDto.FirstName,
                LastName = contactUserDto.LastName,
                Email = contactUserDto.Email,
                PhoneNumber = contactUserDto.PhoneNumber
            };

            // Add the new contact to the repository layer.
            await _contactRepository.CreateContactAsync(contact);

            // Return the new contact to the controller layer.
            return contact;
        }
        public async Task<ContactUser> UpdateContactAsync(int id)
        {
            var check = await _contactRepository..GetSingleContactById(id);
            if (check == null)
            {
                return NotFoundResult();
            }
            else
            {
                check.FirstName = contactUserDto.FirstName;
                check.LastName = contactUserDto.LastName;
                check.Email = contactUserDto.Email;
                check.PhoneNumber = contactUserDto.PhoneNumber;
            }
            var contact = await _contactRepository.UpdateUserAsync();



            await _contactRepository.UpdateContactAsync(contact);

            return contact;
        }*/
    }
}
