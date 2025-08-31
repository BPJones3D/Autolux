using Autolux.Identity.Domain.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Autolux.Identity.Infrastructure.Configuration;

public class ClaimConfiguration : IEntityTypeConfiguration<Claim>
{
    public void Configure(EntityTypeBuilder<Claim> builder)
    {
        builder.ToTable(nameof(Claim));

        builder.Property(x => x.Key).IsRequired();
        builder.Property(x => x.Value).IsRequired();

        builder.Property(x => x.IsDeleted).HasDefaultValue(false);

        builder.HasKey(x => x.Id);
    }
}

