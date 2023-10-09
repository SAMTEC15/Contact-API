using ContactAppAPI.Domain.Model;
using Microsoft.AspNetCore.Builder;
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
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var todataScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = todataScope.ServiceProvider.GetService<ContactUserDbContext>();
                if (!context.ContactUsers.Any())
                {
                    context.ContactUsers.AddRange(new ContactUser()
                    {
                        FirstName = "John",
                        LastName = "Paul",
                        Email = "John@gmailcom",
                        PhoneNumber = "1234567890",
                        PasswordHash = "12345",
                        NormalizedUserName = "28 Benin",
                        UserName = "JohnP"
                    },
                    new ContactUser()
                    {
                        FirstName = "James",
                        LastName = "Paul",
                        Email = "John@gmailcom",
                        PhoneNumber = "1234567890",
                        PasswordHash = "12345",
                        NormalizedUserName = "28 Benin",
                        UserName = "JohnP"
                    });
                    //context.Contacts.AddRange(contacts);
                    context.SaveChanges();
                }
            }
        }

    }
}
