using circles.domain.Abstractions;
using circles.domain.Activities.Events;
using circles.domain.Circles;

namespace circles.domain.Activities;
public class CircleActivity : BaseEntity
{
    internal CircleActivity() { }

    internal CircleActivity(Circle circle, string denomination)
    {
        Denomination = denomination;
        Circle = circle;
    }

    public Circle Circle { get; set; }
    public string Denomination { get; set; } = string.Empty;

    public static CircleActivity Create(Circle circle, string denomination)
    {
        var item = new CircleActivity(circle, denomination);
        item.Id = Guid.NewGuid();
        item.RaiseDomainEvent(new ActivityIsCreatedEvent(item.Id));
        return item;
    }

}
