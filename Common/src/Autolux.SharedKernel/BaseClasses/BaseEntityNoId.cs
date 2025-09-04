namespace Autolux.SharedKernel.BaseClasses;
public abstract class BaseEntityNoId
{
    public bool IsDeleted { get; private set; }

    public DateTime Created { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime Modified { get; set; }
    public Guid ModifiedBy { get; set; }

    public void MarkAsDeleted()
    {
        IsDeleted = true;
    }
}
