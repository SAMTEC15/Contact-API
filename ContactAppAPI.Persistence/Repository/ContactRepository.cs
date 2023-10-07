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
        public async Task<ContactUser> AddNewUserAsync(ContactUser contactUser)
        {
            await _contactUserDbContext.ContactUsers.AddAsync(contactUser);
            await _contactUserDbContext.SaveChangesAsync();

            return contactUser;
        }

        public async Task<ContactUser> GetSingleContactById(int id)
        {
            try
            {
                var check = await _contactUserDbContext.ContactUsers.FindAsync(id);

                if (check == null)
                {
                    return null;
                }
                else
                {
                    return check;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message} An error occurred");
            }
            
        }     

        public async Task<IEnumerable<ContactUser>> GetAllContactsAsync()
        {
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

        public async Task<ContactUser> UpdateUserAsync(int Id, [FromBody] ContactUserDto contact)
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
