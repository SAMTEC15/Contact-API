using Microsoft.AspNetCore.Identity;

namespace ContactAppAPI.Domain.Model
{
    public class ContactUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImageUrl { get; set; }
        
    }
}
