using ContactAppAPI.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactAppAPI.Application.Implementation.Interface
{
    public interface IRegisterServices
    {
        Task<bool> CreateContact(RegDto regDto);
    }
}
