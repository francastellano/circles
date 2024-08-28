using circles.domain.Abstractions;
using circles.domain.Circles;
using circles.domain.Members.Events;

namespace circles.domain.Members;

public class CircleMember : BaseEntity
{
    internal CircleMember() { }

    internal CircleMember(Circle circle, string email)
    {
        Email = email;
        Circle = circle;
    }

    public Circle Circle { get; set; }
    public string Email { get; set; } = string.Empty;

    public static CircleMember Create(Circle circle, string email)
    {
        var item = new CircleMember(circle, email);
        item.Id = Guid.NewGuid();
        item.RaiseDomainEvent(new CircleMemberCreatedEvent(item.Id));
        return item;
    }

}