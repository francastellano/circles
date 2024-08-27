using circles.application.Exceptions;
using circles.domain.MemberSkills;
using circles.domain.Skills.Events;
using circles.infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace circles.application.Features.MemberSkills.Events;

public sealed record AddMembersToNewSkillEvent : INotificationHandler<SkillCreatedEvent>
{

    private readonly CirclesDbContext _dbContext;
    public AddMembersToNewSkillEvent(CirclesDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task Handle(SkillCreatedEvent notification, CancellationToken cancellationToken)
    {
        var skill = await _dbContext.CircleSkills.FirstOrDefaultAsync(e => e.Id == notification.Id, cancellationToken);
        if (skill is null)
            throw new ItemCantBeFoundException("Skill");

        var circleMembers = await _dbContext.CircleMembers.Where(e => e.Circle.Id == skill.Circle.Id).ToListAsync(cancellationToken);

        foreach (var circleMember in circleMembers)
        {
            var skillMember = MemberSkill.Create(circleMember, skill);
            await _dbContext.AddAsync(skillMember, cancellationToken);
        }
        await _dbContext.SaveChangesAsync(cancellationToken);

    }
}