using circles.domain.Abstractions;
using circles.domain.Circles;
using circles.domain.Skills;

namespace circles.domain.MemberSkills;

public class MemberSkill : BaseEntity
{
    internal MemberSkill() { }

    internal MemberSkill(Circle circle, CircleSkill skill)
    {
        Skill = skill;
        Circle = circle;
    }

    public Circle Circle { get; set; }
    public CircleSkill Skill { get; set; }

    public static MemberSkill Create(Circle circle, CircleSkill skill)
    {
        var item = new MemberSkill(circle, skill);
        return item;
    }

}