namespace TP1.Models;

public class Location {
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Address { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
    public required int Capacity { get; set; }
}
