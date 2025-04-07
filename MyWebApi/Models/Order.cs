namespace MyWebApi.Models;

public class Order
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public virtual Customer? Customer { get; set; }
    public DateTime? OrderDate { get; set; }
    public string? OrderNumber { get; set; }
    public decimal TotalAmount { get; set; }
    
    // État de la commande
    public OrderStatus Status { get; set; }
    
    // Adresse de livraison pour cette commande spécifique
    public Address? ShippingAddress { get; set; }
    
    public virtual ICollection<OrderItem> OrderItems { get; set; }
    
    public Order()
    {
        OrderItems = new HashSet<OrderItem>();
    }
}