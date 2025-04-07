namespace MyWebApi.Models
{
    public class Product
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; } // Nullable
        public DateTime CreatedAt { get; set; }
        public bool IsAvailable { get; set; }
    }
}
