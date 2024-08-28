using circles.application.Exceptions;
using circles.domain.Members.Events;
using circles.domain.MemberSkills;
using circles.domain.Skills.Events;
using circles.infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace circles.application.Features.MemberSkills.Events;

public sealed record AddMemberSkillWhenNewMemberEvent : INotificationHandler<CircleMemberCreatedEvent>
{

    private readonly CirclesDbContext _dbContext;
    public AddMemberSkillWhenNewMemberEvent(CirclesDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task Handle(CircleMemberCreatedEvent notification, CancellationToken cancellationToken)
    {
        var member = await _dbContext.CircleMembers.FirstOrDefaultAsync(e => e.Id == notification.Id, cancellationToken);
        if (member is null)
            throw new ItemCantBeFoundException("Member");

        var circleSkills = await _dbContext.CircleSkills.Where(e => e.Circle.Id == member.Circle.Id).ToListAsync(cancellationToken);

        foreach (var skill in circleSkills)
        {
            var skillMember = MemberSkill.Create(member, skill);
            await _dbContext.AddAsync(skillMember, cancellationToken);
        }
        await _dbContext.SaveChangesAsync(cancellationToken);

    }
}