using PhotoStore.Domain.Enums;

namespace PhotoStore.Domain.Entities;

public class Order
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid UserId { get; set; }

    public OrderStatus Status { get; set; } = OrderStatus.Pending;

    public decimal TotalPrice { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


 
    public User User { get; set; } = null!;
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
}