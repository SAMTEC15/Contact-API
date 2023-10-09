using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactAppAPI.Domain.Model
{
    public class PaginationUser
    {
        public int TotalUser { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public List<ContactUser> Users { get; set; }
    }
}
