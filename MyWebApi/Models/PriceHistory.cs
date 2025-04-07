namespace MyWebApi.Models;

public class PriceHistory
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public virtual Product Product { get; set; }
    public decimal OldPrice { get; set; }
    public decimal NewPrice { get; set; }
    public DateTime ChangeDate { get; set; }
    public string ChangedBy { get; set; }
}