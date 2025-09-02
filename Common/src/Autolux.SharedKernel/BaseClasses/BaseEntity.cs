namespace Autolux.SharedKernel.BaseClasses;
public abstract class BaseEntity
{
    public Guid Id { get; protected set; } = Guid.NewGuid();
    public bool IsDeleted { get; private set; } = false;

    public DateTime Created { get; set; } = DateTime.UtcNow;
    public Guid CreatedBy { get; set; } = Guid.Empty;
    public DateTime Modified { get; set; } = DateTime.UtcNow;
    public Guid ModifiedBy { get; set; } = Guid.Empty;

    public void MarkAsDeleted()
    {
        IsDeleted = true;
    }
}