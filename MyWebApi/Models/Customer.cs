namespace MyWebApi.Models;

public class Customer
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    
    // Owned entities pour les adresses
    public Address ShippingAddress { get; set; }
    public Address BillingAddress { get; set; }
    
    // Relations 
    public virtual ICollection<Order> Orders { get; set; }
    public virtual ICollection<ProductReview> Reviews { get; set; }
    
    // Propriétés d'audit
    public DateTime CreatedAt { get; set; }
    public DateTime? LastModified { get; set; }
    
    public Customer()
    {
        Orders = new HashSet<Order>();
        Reviews = new HashSet<ProductReview>();
    }
}