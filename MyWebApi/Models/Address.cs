using Microsoft.EntityFrameworkCore;

namespace MyWebApi.Models;

// Owned entity pour les adresses
[Owned]
public class Address
{
    public string? Street { get; set; }
    public string? City { get; set; }
    public string? ZipCode { get; set; }
    public string? Country { get; set; }
    public string? State { get; set; }
}