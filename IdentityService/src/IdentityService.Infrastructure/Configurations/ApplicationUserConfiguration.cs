using IdentityService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityService.Infrastructure.Configurations;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.IsActive).HasDefaultValue(true);

        builder.OwnsOne(x => x.Email, email =>
        {
            email.Property(e => e.Value).HasColumnName("Email").IsRequired().HasMaxLength(150);
            email.HasIndex(e => e.Value).IsUnique();
        });

        builder.OwnsOne(x => x.Password, pass =>
        {
            pass.Property(p => p.HashedValue).HasColumnName("PasswordHash").IsRequired();
        });
    }
}

