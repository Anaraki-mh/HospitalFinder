using HospitalFinder.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace HospitalFinder.API.DTOs
{
    public class HospitalUpdateReadDto
    {
        public int Id { get; set; }
        public HospitalUpdateOperation OperationType { get; set; }

        [MaxLength(30)]
        public string Name { get; set; }

        [MaxLength(40)]
        public string Country { get; set; }

        [MaxLength(40)]
        public string City { get; set; }

        [MaxLength(150)]
        public string? Address { get; set; }

        public double? Latitude { get; set; }

        public double? Longtitude { get; set; }

        public int? OpenTime { get; set; }

        public int? CloseTime { get; set; }

        public long? Telephone { get; set; }

        [MaxLength(30)]
        public string? Website { get; set; }

        public int HospitalId { get; set; }

    }
}
