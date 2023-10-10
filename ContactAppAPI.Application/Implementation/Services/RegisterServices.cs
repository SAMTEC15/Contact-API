using ContactAppAPI.Application.Implementation.Interface;
using ContactAppAPI.Domain.DTO;
using ContactAppAPI.Domain.Model;
using Microsoft.AspNetCore.Identity;

namespace ContactAppAPI.Application.Implementation.Services
{
    public class RegisterServices :IRegisterServices
    {
        private readonly UserManager<ContactUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RegisterServices(UserManager<ContactUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<bool> CreateContact(RegDto regDto)
        {
            var contactUser = new ContactUser
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = regDto.Firstname,
                LastName = regDto.Lastname,
                Email = regDto.Email,
                PhoneNumber = regDto.PhoneNumber,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                UserName = regDto.Username,
                PasswordHash = regDto.Password,
                Gender = regDto.Gender
            };

           
            try
            {
                var result = await _userManager.CreateAsync(contactUser);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(contactUser, regDto.UserRole.ToString());
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            //}
            return false;
        }
    }
}
