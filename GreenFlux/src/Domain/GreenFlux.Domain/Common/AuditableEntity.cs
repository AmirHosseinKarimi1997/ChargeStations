namespace GreenFlux.Domain.Common;

public abstract class AuditableEntity
{
    public DateTime Created { get; private set; }

    public DateTime? LastModified { get; private set; }

    public bool IsDeleted { get; private set; }

    public DateTime? DeletedAt { get; private set; }


    public void SetCreated(DateTime created)
    {
        this.Created = created;
    }

    public void SetLastModified(DateTime lastModified)
    {
        this.LastModified = lastModified;
    }

    public void SetDeletedAt(DateTime deletedAt)
    {
        this.DeletedAt = deletedAt;
    }

    public void SetIsDeleted(bool isDeleted)
    {
        this.IsDeleted = isDeleted;
    }



}
