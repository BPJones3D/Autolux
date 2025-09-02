using Autolux.Identity.Domain.Permissions;
using Autolux.Identity.Domain.Roles;
using Microsoft.EntityFrameworkCore;

namespace Autolux.Identity.Infrastructure;
public class IdentityDbContext(DbContextOptions<IdentityDbContext> options) : DbContext(options)
{
    //public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    //public DbSet<UserRole> UserRoles { get; set; }
    //public DbSet<RolePermission> RolePermissions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IdentityDbContext).Assembly);
    }

    public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
}
