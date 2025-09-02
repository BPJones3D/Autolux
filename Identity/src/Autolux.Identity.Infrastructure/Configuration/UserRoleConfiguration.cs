using Autolux.Identity.Domain.Roles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Autolux.Identity.Infrastructure.Configuration;
public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.ToTable(nameof(UserRole));

        builder.HasKey(x => new { x.UserId, x.RoleId });

        //builder.HasOne(x => x.User).WithMany(x => x.UserRoles).HasForeignKey(x => x.RoleId);
    }
}