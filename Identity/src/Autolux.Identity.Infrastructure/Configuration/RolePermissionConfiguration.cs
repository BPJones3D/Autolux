using Autolux.Identity.Domain.Roles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Autolux.Identity.Infrastructure.Configuration;

public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.ToTable(nameof(RolePermission));

        builder.HasKey(x => new { x.RoleId, x.PermissionId });

        builder.HasOne(x => x.Role).WithMany(x => x.RolePermissions).HasForeignKey(x => x.RoleId);
    }
}
