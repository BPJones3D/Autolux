using Autolux.Identity.Domain.Users;
using Autolux.SharedKernel.SharedObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Autolux.Identity.Infrastructure.Configuration;

public class UserClaimConfiguration : IEntityTypeConfiguration<UserClaim>
{
    public void Configure(EntityTypeBuilder<UserClaim> builder)
    {
        builder.ToTable(nameof(UserClaim));

        builder.OwnsOne(x => x.Claim, rp =>
        {
            rp.Property(p => p.Value).HasColumnName("PermissionValue");
            rp.Property(p => p.Key)
                .HasColumnName("PermissionKey")
                .HasConversion(p => p.Value, p => PermissionKey.FromValue(p));
        });

        builder.HasQueryFilter(x => !x.Claim.IsDeleted);

        builder.HasKey(x => new { x.UserId, x.ClaimId });
    }
}
