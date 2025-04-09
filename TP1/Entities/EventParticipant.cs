namespace TP1.Models
{
    public class EventParticipant
    {
        public required int EventId { get; set; }
        public required int ParticipantId { get; set; }

        // Ajoutez cette propriété pour la relation avec l'entité Event
        public Event Event { get; set; }  // Relation de navigation vers Event
        public Participant Participant { get; set; }  // Relation de navigation vers Participant

        public DateTime RegistrationDate { get; set; }
        public string? AttendanceStatus { get; set; }
    }
}
