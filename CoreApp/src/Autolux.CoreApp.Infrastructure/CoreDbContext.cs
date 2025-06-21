using Autolux.CoreApp.Domain.Cars;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CoreDbContext(DbContextOptions<CoreDbContext> options) : DbContext(options)
{
    public DbSet<Car> Cars { get; set; }

    private static void ApplyConfigurationForTemplate(EntityTypeBuilder<Car> entity)
    {
        // it is important to consider the following carefully based on the values your entity has

        entity.ToTable("Cars");
        entity.HasKey(e => e.Id);
        entity.Property(e => e.Id).ValueGeneratedOnAdd();

        entity.Property(e => e.Brand).HasMaxLength(100).IsRequired();
        entity.Property(e => e.Name).HasMaxLength(100).IsRequired();
        entity.Property(e => e.Year).IsRequired();
        entity.Property(e => e.Price).IsRequired();
    }
}
