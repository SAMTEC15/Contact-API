using ContactAppAPI.Application.DTO;
using ContactAppAPI.Domain.Model;
using ContactAppAPI.Persistence.DataContext;
using ContactAppAPI.Persistence.Repository.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ContactAppAPI.Persistence.Repository.Services
{
    public class ContactRepository : IContactRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ContactUserDbContext _contactUserDbContext;
        private readonly UserManager<ContactUser> _userManager;

        public ContactRepository(IConfiguration configuration, ContactUserDbContext contactUserDbContext)
        {
            _configuration = configuration;
            _contactUserDbContext = contactUserDbContext;
        }
        public async Task<ContactUser> AddNewUserAsync(ContactUserDto contactUserDto)
        {
            var user = _contactUserDbContext.ContactUsers.FirstOrDefault(u => u.Email == contactUserDto.Email);
            if (user != null)
            {
                return null;

            }

            var contactUser = new ContactUser()
            {

                FirstName = contactUserDto.FirstName,
                LastName = contactUserDto.LastName,
                Email = contactUserDto.Email,
                PhoneNumber = contactUserDto.PhoneNumber,


                /*Address = contactUserDto.Address,
                Company = contactUserDto.Company,
                DateOfBirth = contactUserDto.DateOfBirth,
                Gender = contactUserDto.Gender,
                JobTitle = contactUserDto.JobTitle,
                Notes = contactUserDto.Notes,*/
            };

            await _contactUserDbContext.ContactUsers.AddAsync(contactUser);
            await _contactUserDbContext.SaveChangesAsync();

            return contactUser;

        }

        public async Task<ContactUser> GetSingleContactById(string id)
        {
            try
            {
                var check = await _contactUserDbContext.ContactUsers.FirstOrDefaultAsync(i => i.Id == id);

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
                return check;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message} An error occurred");
            }

        }

        public async Task<ContactUser> GetSingleContactByNumber(string number)
        {
            try
            {
                var check = await _contactUserDbContext.ContactUsers.FirstOrDefaultAsync(i => i.PhoneNumber == number);

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
                return check;
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
                var check = await _contactUserDbContext.ContactUsers.FirstOrDefaultAsync(i => i.Email == email);



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
                return check;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message} An error occurred");
            }
        }
        public async Task<IEnumerable<ContactUser>> GetAllContactsAsync(int page, int pageSize)
        {
            /* var totalUsers = await _userManager.Users.CountAsync();
             var totalPages = (int)Math.Ceiling(totalUsers / (double)pageSize);
             page = Math.Max(1, Math.Min(totalPages, page));

             var users = await _userManager.ContactUser
                 .OrderBy(i => i.Id)
                 .Skip(page - 1) * pageSize)
                 .Take(pageSize)
                 .ToListAsync();

             var paginatedResult = new PaginatedUser
             {
                 totalUsers = totalUsers,
                 CurrentPage = page,
                 pageSize = pageSize,
                 users = users

             };
             return paginatedResult;*/
            return await _contactUserDbContext.ContactUsers.ToListAsync();

        }
        public async Task<ContactUser> DeleteSingleContactByIdAsync(int id)
        {
            var contact = await _contactUserDbContext.ContactUsers.FindAsync(id);
            if (contact == null)
            {
                return null;
            }
            _contactUserDbContext.Remove(contact);
            await _contactUserDbContext.SaveChangesAsync();
            return contact;
        }

        public async Task<IEnumerable<ContactUser>> DeleteAllContact()
        {
            var contacts = await _contactUserDbContext.ContactUsers.ToListAsync();

            foreach (var contact in contacts)
            {
                _contactUserDbContext.Remove(contact);
            }
            await _contactUserDbContext.SaveChangesAsync();

            return contacts;
        }

        public async Task<ContactUser> UpdateUserAsync(int Id, [FromBody] ContactUserDto contactUserDto)
        {
            var contactToUpdate = await _contactUserDbContext.ContactUsers.FindAsync(Id);
            if (contactToUpdate == null)
            {
                return null;
            }
            var updated = _contactUserDbContext.ContactUsers.Update(contactToUpdate);
            return contactToUpdate;
        }


    }
}
