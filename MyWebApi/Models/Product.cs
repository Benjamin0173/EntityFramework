using MyWebApi.Models;

public class Product
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    
    // Gestion des stocks
    public int StockQuantity { get; set; }
    public int ReorderLevel { get; set; }
    public bool IsAvailable { get; set; }
    
    // Relations
    public int CategoryId { get; set; }
    public Category? Category { get; set; }
    public virtual ICollection<ProductReview>? Reviews { get; set; }
    public virtual ICollection<PriceHistory>? PriceHistory { get; set; }
    public virtual ICollection<OrderItem>? OrderItems { get; set; }
    
    // Propriétés d'audit
    public DateTime CreatedAt { get; set; }
    public DateTime? LastModified { get; set; }
    
    public Product()
    {
       // this.Reviews = new HashSet<ProductReview>();
      //  PriceHistory = new HashSet<PriceHistory>();
      //  OrderItems = new HashSet<OrderItem>();
     // this.Reviews = new HashSet<ProductReview>();
    }
}