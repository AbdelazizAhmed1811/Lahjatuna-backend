using System.ComponentModel.DataAnnotations;

namespace LahjatunaAPI.Dtos
{
    public class SendResetPasswordDto
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        //[Required]
        //public string? ClientURI { get; set; }

    }
}
