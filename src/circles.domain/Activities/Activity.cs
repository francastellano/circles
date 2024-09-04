using circles.domain.Abstractions;
using circles.domain.Activities.Events;
using circles.domain.ActivityLocations;
using circles.domain.Circles;

namespace circles.domain.Activities;
public class CircleActivity : BaseEntity
{
    internal CircleActivity() { }

    internal CircleActivity(Circle circle, CircleLocation location, string denomination)
    {
        Denomination = denomination;
        Circle = circle;
        Location = location;
    }

    public Circle Circle { get; set; }
    public CircleLocation Location { get; set; }
    public string Denomination { get; set; } = string.Empty;
    public ActivityStatus Status { get; set; }

    public static CircleActivity Create(Circle circle, CircleLocation location, string denomination)
    {
        var item = new CircleActivity(circle, location, denomination);
        item.Id = Guid.NewGuid();
        item.RaiseDomainEvent(new ActivityIsCreatedEvent(item.Id));
        return item;
    }

}
