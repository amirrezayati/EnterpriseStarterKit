using IdentityService.Domain.Entities;

namespace IdentityService.Domain.Interfaces;

public interface IRoleRepository
{
    Task<ApplicationRole?> GetByNameAsync(string roleName);
    Task<ApplicationRole?> GetByIdAsync(Guid id);
    Task AddAsync(ApplicationRole role);
    Task<List<ApplicationRole>> GetAllAsync();
    Task<bool> ExistsAsync(string roleName);
}
