using TP1.DTOs.ParticipantDTOs;
namespace TP1.DTOs.EventDTOs
{
    public class EventDTO
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Status { get; set; }
        public string? Category { get; set; }
        public int LocationId { get; set; }

        // Ajout de la propriété Participants dans le DTO
        public List<ParticipantDTO> Participants { get; set; } = new List<ParticipantDTO>();
    }
}
