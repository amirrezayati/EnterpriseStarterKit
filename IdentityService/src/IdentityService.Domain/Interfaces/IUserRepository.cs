using IdentityService.Domain.Entities;

namespace IdentityService.Domain.Interfaces;

public interface IUserRepository
{
    Task<ApplicationUser?> GetByEmailAsync(string email);
    Task<ApplicationUser?> GetByIdAsync(Guid id);
    Task AddAsync(ApplicationUser user);
    Task<List<ApplicationUser>> GetAllAsync();
    Task<bool> ExistsAsync(string email);
}