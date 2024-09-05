using circles.domain.Abstractions;
using circles.domain.Activities.Events;
using circles.domain.ActivityLocations;
using circles.domain.Circles;

namespace circles.domain.Activities;
public class CircleActivity : BaseEntity
{
    internal CircleActivity() { }

    internal CircleActivity(Circle circle, CircleLocation location, string denomination, DateTime activityInit)
    {
        Denomination = denomination;
        Circle = circle;
        Location = location;
        ActivityInit = activityInit;
    }

    public Circle Circle { get; set; }
    public CircleLocation Location { get; set; }
    public string Denomination { get; set; } = string.Empty;
    public ActivityStatus Status { get; set; }
    public DateTime ActivityInit { get; set; }

    public static CircleActivity Create(Circle circle, CircleLocation location, string denomination, DateTime activityInit)
    {
        var item = new CircleActivity(circle, location, denomination, activityInit);
        item.Id = Guid.NewGuid();
        item.Status = ActivityStatus.Draft;
        item.RaiseDomainEvent(new ActivityIsCreatedEvent(item.Id));
        return item;
    }

}
