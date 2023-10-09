using ContactAppAPI.Application.DTO;
using ContactAppAPI.Application.Implementation.Interface;
using ContactAppAPI.Application.Implementation.Services;
using ContactAppAPI.Domain.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ContactAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly IContactServices _contactServices;

        public ApplicationController(IContactServices contactServices)
        {
            _contactServices = contactServices;
        }

        // GET: api/users/{id}
        [HttpGet("Users/Admin/id")]
        public async Task<IActionResult> GetContactByIdAsync(string id)
        {
            try
            {
                var contact = await _contactServices.GetContactByIdAsync(id);

                if (contact == null)
                {
                    return new NotFoundObjectResult($"Contact with ID {id} not found.");
                }
                return new OkObjectResult(contact);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult($"{ex.Message} An error occurred while fetching contact by ID. Please try again later.");
            }

        }

        // GET: api/users/{id}
        [HttpGet("Users/Admin/Email")]
        public async Task<IActionResult> GetContactByEmailAsync(string email)
        {
            try
            {
                var contact = await _contactServices.GetContactByIdAsync(email);

                if (contact == null)
                {
                    return new NotFoundObjectResult($"Contact with {email} email not found.");
                }
                return new OkObjectResult(contact);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult($"{ex.Message} An error occurred while fetching contact by ID. Please try again later.");
            }
        }

        // GET: api/users/{id}
        [HttpGet("Users/Admin/Number")]
        public async Task<IActionResult> GetContactByNumberAsync(string number)
        {
            try
            {
                var contact = await _contactServices.GetContactByIdAsync(number);

                if (contact == null)
                {
                    return new NotFoundObjectResult($"Contact with {number} Number not found.");
                }
                return new OkObjectResult(contact);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult($"{ex.Message} An error occurred while fetching contact by ID. Please try again later.");
            }
        }

        // GET: api/Admin
        [HttpGet("Admin")]
        public async Task<IActionResult> GetAllUserAsync(int page, int pageSizes)
        {
            var contacts = await _contactServices.GetAllContactsAsync(page, pageSizes);
            var contactsDto = from contact in contacts
                              select new ContactUserDto()
                              {
                                  Id = Guid.Parse(contact.Id),
                                  FirstName = contact.FirstName,
                                  LastName = contact.LastName,
                                  Email = contact.Email,
                                  PhoneNumber = contact.PhoneNumber,
                              };
            return Ok(contactsDto);

        }

        [HttpPost("AddNewUser")]
        public async Task<IActionResult> AddNewUserAsync([FromBody] ContactUserDto contactUserDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Input");
            }
            var contactUser = await _contactServices.AddNewUserAsync(contactUserDto);

            return Ok(contactUser);

        }

        [HttpPut("update-contact/{id}")]
        public async Task<IActionResult> UpdateContactAsync(string id, [FromBody] ContactUserDto contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest($"Contact with ID {id} not found.");
            }
            var updatedContact = await _contactServices.UpdateUserAsync(id, contact);

            return new OkObjectResult(updatedContact);

        }

        [HttpDelete("Delete-Contact/{id}")]
        public async Task<IActionResult> DeleteSingleContactByIdAsync(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid contact ID.");
            }
            var check = await _contactServices.DeleteSingleContactByIdAsync(id);
            if(check == null)
            {
                return NotFound("Does not Exist");
            }
            return Ok("Deleted Succsssfully");
        }

        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteAllUserAsync()
        {
            var result = await _contactServices.DeleteAllContact();
            return Ok("All Details has been successfully Deleted from the database");
        }

        /*const int maxPageSize = 20;
        public constructor()
        {
            for(int i = 0; i < 10; i++)
            {
                DBNull.Add(new ContactUser { Id = i, Name }); 
            }
        }
        public IEnumerable<ContactUser> GetPagination(int page = 1, int pageSize = 5)
        {
            if(pageSize > maxPageSize)
            {
                pageSize == maxPageSize;
            }

        }*/
    }
}
