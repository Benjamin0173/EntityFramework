namespace MyWebApi.Models;

public class OrderItem
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public virtual Order Order { get; set; }
    public int ProductId { get; set; }
    public virtual Product Product { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; } // Prix au moment de l'achat
    public decimal TotalPrice { get; set; } // Prix * Quantité
    public decimal? DiscountAmount { get; set; }
}