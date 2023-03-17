using System.ComponentModel.DataAnnotations;

namespace HospitalFinder.API.DTOs
{
    public class LoginRequestDto
    {
        [Required]
        [MaxLength(50)]
        public string? Email { get; set; }

        [Required]
        [MinLength(6)]
        public string? Password { get; set; }
    }
}
