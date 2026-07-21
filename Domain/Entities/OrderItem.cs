namespace PhotoStore.Domain.Entities;

public class OrderItem
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid OrderId { get; set; }

    public Guid PhotoId { get; set; }

    public decimal Price { get; set; }
    
    public string PhotoTitle { get; set; } = string.Empty;




    public Order Order { get; set; } = null!;
    public Photo Photo { get; set; } = null!;
    public Download? Download { get; set; }
}