namespace TP1.Models
{
    public class Event
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Status { get; set; }
        public string? Category { get; set; }
        
        public int LocationId { get; set; }

        // EventParticipants devient optionnel, pour permettre une création sans participants
        public ICollection<EventParticipant>? EventParticipants { get; set; } = new List<EventParticipant>();
    }
}
