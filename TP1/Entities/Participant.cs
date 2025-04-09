namespace TP1.Models;

public class Participant {
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public string? Company { get; set; }
    public string? JobTitle { get; set; }

    public ICollection<EventParticipant>? EventParticipants { get; set; } = new List<EventParticipant>();
}
