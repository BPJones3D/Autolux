using Autolux.Identity.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Autolux.Identity.Infrastructure.Configuration;
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(nameof(User));

        builder.Property(x => x.Email).IsRequired();
        builder.Property(x => x.NormalizedEmail).IsRequired();
        builder.Property(x => x.Password).IsRequired();

        builder.HasIndex(x => x.NormalizedEmail);

        builder.HasKey(x => x.Id);
    }
}