using circles.api.contracts.Goals.Commands;
using circles.application.Exceptions;
using circles.domain.Goals;
using circles.infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace circles.application.Features.Goals.Commands;

public sealed record CircleGoalAddCommand(CircleGoalAddRequest Params) : IRequest;

internal sealed record CircleGoalAddCommandHandler(CirclesDbContext DbContext) : IRequestHandler<CircleGoalAddCommand>
{
    public async Task Handle(CircleGoalAddCommand request, CancellationToken cancellationToken)
    {
        var circle = await DbContext.Circles.FirstOrDefaultAsync(e => e.Id == request.Params.CircleId, cancellationToken: cancellationToken);
        if (circle is null)
            throw new ItemCantBeFoundException("Circle");

        var item = CircleGoal.Create(circle, request.Params.Denomination);

        await DbContext.AddAsync(item, cancellationToken);
        await DbContext.SaveChangesAsync(cancellationToken);
    }
}