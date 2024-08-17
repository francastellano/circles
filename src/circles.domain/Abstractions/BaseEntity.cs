namespace circles.domain.Abstractions;

public abstract class BaseEntity
{

    protected readonly List<IDomainEvent> DomainEvents = [];
    public IReadOnlyList<IDomainEvent> GetDomainEvents()
    {
        return DomainEvents.ToList();
    }

    public void ClearDomainEvents()
    {
        DomainEvents.Clear();
    }

    public void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        DomainEvents.Add(domainEvent);
    }
}