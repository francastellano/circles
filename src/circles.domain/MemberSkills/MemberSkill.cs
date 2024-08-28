using circles.domain.Abstractions;
using circles.domain.Members;
using circles.domain.MemberSkills.Events;
using circles.domain.Skills;

namespace circles.domain.MemberSkills;

public class MemberSkill : BaseEntity
{
    internal MemberSkill() { }

    internal MemberSkill(CircleMember member, CircleSkill skill)
    {
        Skill = skill;
        Member = member;
    }

    public CircleMember Member { get; set; }
    public CircleSkill Skill { get; set; }

    public static MemberSkill Create(CircleMember member, CircleSkill skill)
    {
        var item = new MemberSkill(member, skill);
        item.Id = Guid.NewGuid();
        item.RaiseDomainEvent(new MemberSkillCreatedEvent(item.Id));
        return item;
    }

}