using Autolux.Identity.Domain.Roles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Autolux.Identity.Infrastructure.Configuration;
public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable(nameof(Role));

        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.NormalizedName).IsRequired();

        builder.Ignore(x => x.UserRoles);

        builder.Property(x => x.IsDeleted).HasDefaultValue(false);

        builder.HasIndex(x => x.NormalizedName);

        builder.HasKey(x => x.Id);
    }
}
