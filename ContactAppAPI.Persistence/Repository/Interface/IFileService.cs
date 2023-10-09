using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactAppAPI.Persistence.Repository.Interface
{
    public interface IFileService
    {
        public Tuple<int, string> SaveImage(IFormFile imageFile);
       // public bool DeleteImage(string imageFileName);
    }
}
