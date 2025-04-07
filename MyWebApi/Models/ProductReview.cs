namespace MyWebApi.Models;

public class ProductReview
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public virtual Product? Product { get; set; }
    public int CustomerId { get; set; }
    public virtual Customer? Customer { get; set; }
    public int Rating { get; set; } // 1-5 étoiles
    public string? Comment { get; set; }
    public DateTime ReviewDate { get; set; }
    public bool IsApproved { get; set; }
}