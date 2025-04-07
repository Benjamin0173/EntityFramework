
namespace MyWebApi.Models;

public class Category
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    
    // Hiérarchie des catégories
    public int? ParentCategoryId { get; set; }
    public virtual Category? ParentCategory { get; set; }
    public virtual ICollection<Category> SubCategories { get; set; }
    
    // Relation avec les produits
    public virtual ICollection<Product> Products { get; set; }
    
    public Category()
    {
        SubCategories = new HashSet<Category>();
        Products = new HashSet<Product>();
    }
}