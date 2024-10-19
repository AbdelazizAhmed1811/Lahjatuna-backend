// Ignore Spelling: Username Furni Dto

using System.ComponentModel.DataAnnotations;

namespace LahjatunaAPI
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
