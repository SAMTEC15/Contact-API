using ContactAppAPI.Application.DTO;
using ContactAppAPI.Application.Implementation.Interface;
using ContactAppAPI.Domain.Model;
using ContactAppAPI.Persistence.DataContext;
using ContactAppAPI.Persistence.Repository.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactAppAPI.Application.Implementation.Services
{
    public class ContactServices : IContactServices
    {
        private readonly IContactRepository _contactRepository;
        private readonly UserManager<ContactUser> _userManager;
        private readonly ContactUserDbContext _contactUserDbContext;
        private readonly ICloudRep _cloudRep;

        public ContactServices(IContactRepository contactRepository, ContactUserDbContext contactUserDbContext)
        {
            _contactRepository = contactRepository;
            //_userManager = userManager;
            _contactUserDbContext = contactUserDbContext;
        }

        public async Task<string> AddNewUserAsync(ContactUserDto contactUserDto)
        {
            // Validate the user input           

            var user = _contactUserDbContext.ContactUsers.FirstOrDefault(u => u.Email == contactUserDto.Email);
            if (user != null)
            {
                return "User already exist";

            }
            var contactUser = new ContactUserDto()
            {
                FirstName = contactUserDto.FirstName,
                LastName = contactUserDto.LastName,
                Email = contactUserDto.Email,
                PhoneNumber = contactUserDto.PhoneNumber,
                Address = contactUserDto.Address,
                Company = contactUserDto.Company,
                DateOfBirth = contactUserDto.DateOfBirth,
                Gender = contactUserDto.Gender,
                JobTitle = contactUserDto.JobTitle,
                Notes = contactUserDto.Notes,

            };
            await _contactRepository.AddNewUserAsync(contactUser);
            await _contactUserDbContext.SaveChangesAsync();
            return "User Added Successfully";

        }

        public async Task<ContactUser> GetContactByIdAsync(string id)
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

        public async Task<ContactUser> GetSingleContactByNumber(string number)
        {
            try
            {
                var check = await _contactRepository.GetSingleContactByNumber(number);

                if (check == null)
                {
                    return null;
                }
                var data = new ContactUser
                {
                    Id = check.Id,
                    FirstName = check.FirstName,
                    LastName = check.LastName,
                    Email = check.Email,
                    PhoneNumber = check.PhoneNumber,
                    UserName = check.UserName,
                    ImageUrl = check.ImageUrl,
                };
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message} An error occurred");
            }
        }

        public async Task<ContactUser> GetSingleContactByEmail(string email)
        {
            try
            {
                var check = await _contactRepository.GetSingleContactByNumber(email);

                if (check == null)
                {
                    return null;
                }
                var data = new ContactUser
                {
                    Id = check.Id,
                    FirstName = check.FirstName,
                    LastName = check.LastName,
                    Email = check.Email,
                    PhoneNumber = check.PhoneNumber,
                    UserName = check.UserName,
                    ImageUrl = check.ImageUrl,
                };
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message} An error occurred");
            }
        }
        public async Task<IEnumerable<ContactUser>> GetAllContactsAsync(int page, int pageSizes)
        {
            var contacts = await _contactRepository.GetAllContactsAsync(page, pageSizes);

            return contacts;
        }

        public async Task<ContactUser> UpdateUserAsync(string Id, [FromBody] ContactUserDto contact)
        {

            var contactUser = await _contactRepository.GetSingleContactById(Id);
            if (contactUser == null)
            {
                return null;
            }

            contactUser.FirstName = contact.FirstName;
            contactUser.LastName = contact.LastName;
            contactUser.Email = contact.Email;
            contactUser.PhoneNumber = contact.PhoneNumber;

            _contactUserDbContext.Add(contactUser);
            _contactUserDbContext.SaveChanges();

            return contactUser;
        }

        public async Task<string> DeleteSingleContactByIdAsync(string id)
        {
            var check = await _contactRepository.GetSingleContactById(id);
            if (check == null)
            {
                return "Not Found";
            }
            var remove = _contactUserDbContext.Remove(check);
            _contactUserDbContext.SaveChanges();
            return "Successfully deleted";
        }

        public async Task<IEnumerable<ContactUser>> DeleteAllContact()
        {
            var check = await _contactRepository.DeleteAllContact();
            _contactUserDbContext.SaveChanges();
            return check;
        }

        public async Task<string> AddImage(ContactUser model)
        {
            if (model == null)
            {
                return "Invalid Entry";
            }
            _cloudRep.Add(model);
            return "Image Added Successfully";
        }


    }
}
