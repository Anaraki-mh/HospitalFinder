using HospitalFinder.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace HospitalFinder.API.DTOs
{
    public class HospitalCreateDto
    {

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [MaxLength(40)]
        public string Country { get; set; }

        [Required]
        [MaxLength(40)]
        public string City { get; set; }

        [MaxLength(150)]
        public string? Address { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longtitude { get; set; }

        public int? OpenTime { get; set; }

        public int? CloseTime { get; set; }

        public long? Telephone { get; set; }

        [MaxLength(30)]
        public string? Website { get; set; }
    }
}
