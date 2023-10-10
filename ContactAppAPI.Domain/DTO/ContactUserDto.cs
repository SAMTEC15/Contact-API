using ContactAppAPI.Common.Enums;
using ContactAppAPI.Domain.Model;
using Microsoft.AspNetCore.Identity;

namespace ContactAppAPI.Application.DTO
{
    public class ContactUserDto : IdentityUser
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public ContactAddressDto Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public string Company { get; set; }
        public string JobTitle { get; set; }
        public string Notes { get; set; }
        public string ImageUrl { get; set; }        
        public UserRole UserType { get; set; }
        public string HomeAddress { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set;}
        public UserRole UserRole { get; set; }
    }
}
