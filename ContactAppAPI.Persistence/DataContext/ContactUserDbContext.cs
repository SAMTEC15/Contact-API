using ContactAppAPI.Domain.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
