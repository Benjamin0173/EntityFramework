namespace MyWebApi.Models
{
    public class Category
        {
            public int Id { get; set; }
            public required string Name { get; set; }
            public string? Description { get; set; }
            public DateTime CreatedAt { get; set; }
            public bool IsActive { get; set; }
        }
}
