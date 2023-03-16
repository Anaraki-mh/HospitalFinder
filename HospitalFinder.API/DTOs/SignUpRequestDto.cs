using System.ComponentModel.DataAnnotations;

namespace HospitalFinder.API.DTOs
{
    public class SignUpRequestDto
    {
        [Required]
        [MaxLength(50)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
