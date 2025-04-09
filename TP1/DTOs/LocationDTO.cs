using System.ComponentModel.DataAnnotations;

namespace TP1.DTOs.LocationDTOs
{
    public class LocationDTO
    {
        public int? Id { get; set; } // ignoré en POST

        [StringLength(100)]
        public required string Name { get; set; }

        [StringLength(200)]
        public required string Address { get; set; }

        [StringLength(100)]
        public string? City { get; set; }

        [StringLength(100)]
        public string? Country { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "La capacité doit être positive.")]
        public required int Capacity { get; set; }
    }
}
