using Autolux.Identity.Domain.Permissions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Autolux.Identity.Infrastructure.Configuration;

public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable(nameof(Permission));

        //builder.Property(x => x.Id).IsRequired();
        builder.Property(x => x.Value).IsRequired();

        builder.HasKey(x => new { x.Id });
    }
}
