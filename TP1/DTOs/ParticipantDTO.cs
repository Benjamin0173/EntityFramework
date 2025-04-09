using System.ComponentModel.DataAnnotations;

namespace TP1.DTOs.ParticipantDTOs
{
    public class ParticipantDTO
    {
        public int Id { get; set; } // ignoré en POST

        [StringLength(50)]
        public required string FirstName { get; set; }

        [StringLength(50)]
        public required string LastName { get; set; }

        [EmailAddress]
        public required string Email { get; set; }

        [StringLength(100)]
        public string? Company { get; set; }

        [StringLength(100)]
        public string? JobTitle { get; set; }
    }
}
