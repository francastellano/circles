using circles.domain.Abstractions;
using circles.domain.Circles.Events;

namespace circles.domain.Circles;

public sealed class Circle : BaseEntity
{
    internal Circle() { }

    internal Circle(string denomination)
    {
        Id = Guid.NewGuid();
        Denomination = denomination;
    }
    public Guid Id { get; set; }
    public string Denomination { get; set; } = string.Empty;

    public static Circle Create(string denomination)
    {
        Circle circle = new(denomination);
        circle.RaiseDomainEvent(new CircleCreatedEvent(circle.Id));
        return circle;
    }
}
