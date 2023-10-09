using ContactAppAPI.Application.DTO;
using ContactAppAPI.Application.Implementation.Interface;
using ContactAppAPI.Domain.Model;
using ContactAppAPI.Persistence.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ContactAppAPI.Controllers
{
    public class CloudController
    {
        private readonly IFileService _fileService;        
        private readonly IContactServices _contactServices;

        public CloudController(IFileService fs, IContactServices contactServices)
        {
            this._fileService = fs;            
            _contactServices = contactServices;
        }
       /* [HttpPost]
        public async Task <IActionResult> Add([FromForm] ContactUser model)
        {
            *//*var status = new ContactUserDto();
            if (!ModelState.IsValid)
            {               
                return new BadRequestResult();
            }
            if (model.ImageUrl != null)
            {
                var fileResult = await _fileService.SaveImage(model.Id);
                if (fileResult.Item1 == 1)
                {
                    model.ImageUrl = fileResult.Item2; // getting name of image
                }
                
            }
            return Ok(status);*//*

        }*/
    }
}
