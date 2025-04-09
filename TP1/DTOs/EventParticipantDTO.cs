using System.ComponentModel.DataAnnotations;

namespace TP1.DTOs.EventParticipantDTOs
{
    public class EventParticipantDTO
    {
        [Required]
        public int EventId { get; set; }

        [Required]
        public int ParticipantId { get; set; }

        [Required]
        public DateTime RegistrationDate { get; set; }

        [StringLength(100)]
        public string? AttendanceStatus { get; set; }
    }
}
