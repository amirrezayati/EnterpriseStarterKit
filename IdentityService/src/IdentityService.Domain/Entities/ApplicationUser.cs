using IdentityService.Domain.ValueObjects;
using SharedKernel.Base;

namespace IdentityService.Domain.Entities;

public class ApplicationUser : Entity<Guid>, IAggregateRoot
{
    public Email Email { get; private set; } = default!;
    public Password Password { get; private set; } = default!;
    public bool IsActive { get; private set; } = true;
    public DateTime CreatedAt { get; private set; }

    // برای EF Core
    private ApplicationUser() { }

    public ApplicationUser(Email email, Password password)
    {
        Email = email;
        Password = password;
        CreatedAt = DateTime.UtcNow;
    }

    public void ChangePassword(Password newPassword)
    {
        Guard.AgainstNull(newPassword, nameof(newPassword));
        Password = newPassword;
    }

    public void Deactivate() => IsActive = false;

    public void Activate() => IsActive = true;
}
