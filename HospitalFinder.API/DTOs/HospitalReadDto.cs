using System.ComponentModel.DataAnnotations;

namespace HospitalFinder.API.DTOs
{
    public class HospitalReadDto
    {
        public int Id { get; set; }

        [MaxLength(30)]
        public string Name { get; set; }

        [MaxLength(40)]
        public string Country { get; set; }

        [MaxLength(40)]
        public string City { get; set; }

        [MaxLength(150)]
        public string? Address { get; set; }

        public string Latitude { get; set; }

        public string Longtitude { get; set; }

        public int? OpenTime { get; set; }

        public int? CloseTime { get; set; }

        public long? Telephone { get; set; }

        [MaxLength(30)]
        public string? Website { get; set; }

        [MaxLength(75)]
        public string GoogleMapsLink { get; set; }

    }
}
