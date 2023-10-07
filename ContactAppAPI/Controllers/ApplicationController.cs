using ContactAppAPI.Application.DTO;
using ContactAppAPI.Application.Implementation.Interface;
using ContactAppAPI.Application.Implementation.Services;
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



        [HttpGet("get-contact-by-id/{id}")]
        public async Task<IActionResult> GetContactByIdAsync(int id)
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
                // Handle the exception here, log it, and return an appropriate error response.
                return StatusCode(500, $"An error occurred while fetching contact by ID: {ex.Message}");

            }
        }
        [HttpPost("add-new-user")]
        public async Task<IActionResult> AddNewUserAsync([FromBody] ContactUserDto contactUserDto)
        {
            var contactUser = await _contactServices.AddNewUser(contactUserDto);

            return new OkObjectResult(contactUser);

        }

        /*[HttpPut("update-contact/{id}")]
        public async Task<IActionResult> UpdateContactAsync(int id, [FromBody] ContactUserDto contact)
        {
            var updatedContact = await _contactServices.UpdateUserAsync(id, contact);

            if (updatedContact == null)
            {
                return new NotFoundObjectResult($"Contact with ID {id} not found.");
            }

            return new OkObjectResult(updatedContact);

        }*/
    }
}
