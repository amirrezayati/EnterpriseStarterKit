using IdentityService.Domain.Entities;
using IdentityService.Domain.Interfaces;
using IdentityService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Infrastructure.Repositories;

public class UserRepository(ApplicationDbContext context) : IUserRepository
{
    private static readonly Func<ApplicationDbContext, string, Task<ApplicationUser?>> GetByEmailCompiled =
        EF.CompileAsyncQuery((ApplicationDbContext ctx, string email) =>
            ctx.Users.AsNoTracking().FirstOrDefault(u => u.Email.Value == email));

    public Task<ApplicationUser?> GetByEmailAsync(string email)
        => GetByEmailCompiled(context, email);

    public Task<ApplicationUser?> GetByIdAsync(Guid id)
        => context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);

    public Task<List<ApplicationUser>> GetAllAsync()
        => context.Users.AsNoTracking().ToListAsync();

    public Task AddAsync(ApplicationUser user)
        => context.Users.AddAsync(user).AsTask();

    public Task<bool> ExistsAsync(string email)
        => context.Users.AsNoTracking().AnyAsync(u => u.Email.Value == email);
}