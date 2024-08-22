using circles.domain.Abstractions;
using circles.domain.Circles.Events;

namespace circles.domain.Circles;

public sealed class Circle : BaseEntity
{
    internal Circle() { }

    internal Circle(string denomination, string creator)
    {
        Id = Guid.NewGuid();
        Denomination = denomination;
        Creator = creator;
    }
    public string Denomination { get; set; } = string.Empty;
    public string Creator { get; set; } = string.Empty;

    public static Circle Create(string denomination, string email)
    {
        Circle circle = new(denomination, email);
        circle.RaiseDomainEvent(new CircleCreatedEvent(circle.Id));
        return circle;
    }
}
