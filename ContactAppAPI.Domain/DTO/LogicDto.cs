using System.ComponentModel.DataAnnotations;

namespace ContactAppAPI.Application.DTO
{
    public class LogicDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
