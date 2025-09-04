namespace Autolux.SharedKernel.BaseClasses;
public abstract class BaseEntity : BaseEntityNoId
{
    public Guid Id { get; protected set; }
}