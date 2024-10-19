using System.ComponentModel.DataAnnotations;

namespace LahjatunaAPI.Dtos.Auth
{
    public class SendEmailVerificationDto
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

    }
}
