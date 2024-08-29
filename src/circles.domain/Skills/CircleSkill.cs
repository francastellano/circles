using circles.domain.Abstractions;
using circles.domain.Circles;
using circles.domain.Skills.Events;

namespace circles.domain.Skills;

public class CircleSkill : BaseEntity
{
    internal CircleSkill() { }

    internal CircleSkill(Circle circle, string denomination, CircleSkill? mainSkill)
    {
        Denomination = denomination;
        Circle = circle;
        Skill = mainSkill;
    }

    public CircleSkill? Skill { get; set; }
    public Circle Circle { get; set; }
    public string Denomination { get; set; } = string.Empty;


    public static CircleSkill Create(Circle circle, string denomination, CircleSkill? mainSkill)
    {
        var item = new CircleSkill(circle, denomination, mainSkill);
        item.Id = Guid.NewGuid();
        item.RaiseDomainEvent(new SkillCreatedEvent(item.Id));
        return item;
    }
}
