using ContactAppAPI.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactAppAPI.Persistence.Repository.Interface
{
    public interface ICloudRep
    {
        bool Add(ContactUser model);
    }
}
