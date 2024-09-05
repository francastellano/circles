using circles.api.contracts.ActivityMembers.Commands;
using circles.application.Abstractions.Messages;
using circles.application.Exceptions;
using circles.domain.Abstractions;
using circles.domain.ActivityMembers;
using circles.infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace circles.application.Features.ActivityMembers.Commands.Add;

public sealed record ActivityMemberAddCommand(ActivityMemberAddRequest Parameter) : ICommand;

internal sealed record ActivityMemberAddCommandHandler : ICommandHandler<ActivityMemberAddCommand>
{
    private readonly CirclesDbContext _dbContext;
    public ActivityMemberAddCommandHandler(CirclesDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Result> Handle(ActivityMemberAddCommand request, CancellationToken cancellationToken)
    {

        var activity = await _dbContext.CircleActivities.FirstOrDefaultAsync(e => e.Id == request.Parameter.ActivityId, cancellationToken);
        if (activity is null)
            throw new ItemCantBeFoundException("Activity");

        var member = await _dbContext.CircleMembers.FirstOrDefaultAsync(e => e.Id == request.Parameter.MemberId, cancellationToken);
        if (member is null)
            throw new ItemCantBeFoundException("Member");

        var data = ActivityMember.Create(member, activity);

        await _dbContext.ActivityMembers.AddAsync(data, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}