using ContactAppAPI.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace ContactAppAPI.Domain.DTO
{
    public class RegDto
    {
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        public string Username { get; set; }
        public Gender Gender { get; set; }
        public UserRole UserRole { get; set; }
    }
}
