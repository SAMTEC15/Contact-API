﻿using ContactAppAPI.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactAppAPI.Application.Implementation.Interface
{
    public interface ILoginServices
    {
        Task<string> Login(LoginDto loginDto);
    }
}
