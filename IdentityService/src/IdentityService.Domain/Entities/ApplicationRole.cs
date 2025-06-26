using SharedKernel.Base;

namespace IdentityService.Domain.Entities;

public class ApplicationRole : Entity<Guid>, IAggregateRoot
{
    public string Name { get; private set; } = default!;
    public string? Description { get; private set; }

    private ApplicationRole() { }

    public ApplicationRole(string name, string? description = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Role name cannot be empty");

        Name = name;
        Description = description;
    }

    public void Rename(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
            throw new ArgumentException("Role name cannot be empty");

        Name = newName;
    }

    public void ChangeDescription(string? newDescription)
    {
        Description = newDescription;
    }
}