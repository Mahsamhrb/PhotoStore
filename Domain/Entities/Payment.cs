using PhotoStore.Domain.Enums;

namespace PhotoStore.Domain.Entities;
public class Payment
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid OrderId { get; set; }

    public decimal Amount { get; set; }

    public PaymentStatus Status { get; set; }

    public string? TransactionId { get; set; }

    public string Gateway { get; set; } = string.Empty;

    public DateTime? PaidAt { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


    public Order Order { get; set; } = null!;
}