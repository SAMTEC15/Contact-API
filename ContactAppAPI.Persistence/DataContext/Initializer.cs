using ContactAppAPI.Domain.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactAppAPI.Persistence.DataContext
{
    public class Initializer
    {

        public static void SeedRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            if (!roleManager.RoleExistsAsync("ADMIN").Result)
            {
                var role = new IdentityRole("ADMIN");
                roleManager.CreateAsync(role).Wait();
            }
            if (!roleManager.RoleExistsAsync("REGULARUSER").Result)
            {
                var role = new IdentityRole("REGULARUSER");
                roleManager.CreateAsync(role).Wait();
            }
        }


    }
}
