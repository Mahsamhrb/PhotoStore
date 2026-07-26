using Microsoft.AspNetCore.Identity;
using PhotoStore.Domain.Enums;

namespace PhotoStore.Domain.Entities;
public class User : IdentityUser<Guid>
{
    public UserRole Role { get; set; } = UserRole.Customer;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;




    public ICollection<Order> Orders { get; set; } = new List<Order>();
}