using ContactAppAPI.Domain.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace ContactAppAPI.Persistence.DataContext
{
    public class ContactUserDbContext : IdentityDbContext
    {
        public ContactUserDbContext(DbContextOptions<ContactUserDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }
        public DbSet<ContactUser> ContactUsers { get; set; }


    }
}
