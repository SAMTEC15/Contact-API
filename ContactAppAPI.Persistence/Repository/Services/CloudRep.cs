using ContactAppAPI.Domain.Model;
using ContactAppAPI.Persistence.DataContext;
using ContactAppAPI.Persistence.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactAppAPI.Persistence.Repository.Services
{
    public class CloudRep : ICloudRep
    {
        private readonly ContactUserDbContext _contactUserDbContext;
        public CloudRep(ContactUserDbContext context)
        {
            _contactUserDbContext = context;
        }
        public bool Add(ContactUser model)
        {
            try
            {
                _contactUserDbContext.ContactUsers.Add(model);
                _contactUserDbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
    }
}
