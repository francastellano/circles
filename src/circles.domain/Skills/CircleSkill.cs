using circles.domain.Abstractions;
using circles.domain.Circles;
using circles.domain.Skills.Events;

namespace circles.domain.Skills;

public class CircleSkill : BaseEntity
{
    internal CircleSkill() { }

    internal CircleSkill(Circle circle, string denomination)
    {
        Denomination = denomination;
        Circle = circle;
    }

    public Circle Circle { get; set; }
    public string Denomination { get; set; } = string.Empty;

    public static CircleSkill Create(Circle circle, string denomination)
    {
        var item = new CircleSkill(circle, denomination);
        item.RaiseDomainEvent(new SkillCreatedEvent(item.Id));
        return item;
    }
}
