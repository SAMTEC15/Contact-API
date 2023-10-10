using ContactAppAPI.Application.DTO;
using ContactAppAPI.Application.Implementation.Interface;
using ContactAppAPI.Domain.Model;
using ContactAppAPI.Persistence.DataContext;

namespace ContactAppAPI.Application.Implementation.Services
{
    public class ContactDtoMapper : IContactDtoMapper
    {
        private readonly ContactUserDbContext _contactUserDbContext;

        public ContactDtoMapper(ContactUserDbContext contactUserDbContext)
        {
            _contactUserDbContext = contactUserDbContext;
        }


        public List<ContactUserDto> MapContactsToDTOs(List<ContactUser> contacts)
        {
            var contactDtos = new List<ContactUserDto>();
            foreach (var contact in contacts)
            {
                var contactDto = MapContactToContactDTO(contact);
                contactDtos.Add(contactDto);
            }
            return contactDtos;
        }

        public ContactUserDto MapContactToContactDTO(ContactUser contactUser)
        {
            return new ContactUserDto
            {
                //Id = contactUser.Id,
                FirstName = contactUser.FirstName,
                LastName = contactUser.LastName,
                PhoneNumber = contactUser.PhoneNumber,
                PhoneNumberConfirmed = contactUser.PhoneNumberConfirmed,
                Email = contactUser.Email,
                Gender = contactUser.Gender,
                PasswordHash = contactUser.PasswordHash,
                HomeAddress = contactUser.HomeAddress,
                EmailConfirmed = contactUser.EmailConfirmed,
                AccessFailedCount = contactUser.AccessFailedCount,
                ConcurrencyStamp = contactUser.ConcurrencyStamp,
                LockoutEnabled = contactUser.LockoutEnabled,
                LockoutEnd = contactUser.LockoutEnd,
                NormalizedEmail = contactUser.NormalizedEmail,
                NormalizedUserName = contactUser.NormalizedUserName,
                SecurityStamp = contactUser.SecurityStamp,
                ImageUrl = contactUser.ImageUrl,
                UserName = contactUser.UserName,
                TwoFactorEnabled = contactUser.TwoFactorEnabled,
                UserRole = contactUser.UserType,
                CreatedAt = contactUser.CreatedAt,
                UpdatedAt = contactUser.UpdatedAt
            };
        }
    }
}
