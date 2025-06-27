using IdentityService.Domain.Interfaces;
using IdentityService.Infrastructure.Data;
using IdentityService.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityService.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString, o =>
        {
            o.EnableRetryOnFailure();
            o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
        }));

        services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }
}