using ContactAppAPI.Common.Enums;
using Microsoft.AspNetCore.Identity;

namespace ContactAppAPI.Domain.Model
{
    public class ContactUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public Gender Gender { get; set; }
        public UserRole UserType { get; set; }
        public string HomeAddress { get; set; }

    }
}
