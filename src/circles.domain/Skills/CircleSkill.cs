using circles.domain.Abstractions;
using circles.domain.Circles;
using circles.domain.Skills.Events;

namespace circles.domain.Skills;

public class CircleSkill : BaseEntity
{
    internal CircleSkill() { }

    internal CircleSkill(Circle circle, string denomination, CircleSkill? mainSkill, string? description)
    {
        Denomination = denomination;
        Circle = circle;
        Skill = mainSkill;
        Description = description;
    }

    public CircleSkill? Skill { get; set; }
    public Circle Circle { get; set; }
    public string Denomination { get; set; } = string.Empty;
    public string? Description { get; set; }

    public static CircleSkill Create(Circle circle, string denomination, CircleSkill? mainSkill, string? description)
    {
        var item = new CircleSkill(circle, denomination, mainSkill, description);
        item.Id = Guid.NewGuid();
        item.RaiseDomainEvent(new SkillCreatedEvent(item.Id));
        return item;
    }
}
