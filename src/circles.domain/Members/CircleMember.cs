using circles.domain.Abstractions;
using circles.domain.Circles;
using circles.domain.Members.Events;

namespace circles.domain.Members;

public class CircleMember : BaseEntity
{
    internal CircleMember() { }

    internal CircleMember(Circle circle, string email, string name)
    {
        Email = email;
        Circle = circle;
        Name = name;
    }

    public Circle Circle { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;

    public static CircleMember Create(Circle circle, string email, string name)
    {
        var item = new CircleMember(circle, email, name);
        item.Id = Guid.NewGuid();
        item.RaiseDomainEvent(new CircleMemberCreatedEvent(item.Id));
        return item;
    }

}