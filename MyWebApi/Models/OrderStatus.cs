namespace MyWebApi.Models;

// Enumération pour l'état de la commande
public enum OrderStatus
{
    Pending,
    Processing,
    Shipped,
    Delivered,
    Cancelled,
    Returned
}