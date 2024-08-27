using circles.api.contracts.Goals.Commands;
using circles.application.Exceptions;
using circles.domain.Goals;
using circles.infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace circles.application.Features.Goals.Commands;

public sealed record CircleGoalAddCommand(CircleGoalAddRequest Params) : IRequest;

internal sealed record CircleGoalAddCommandHandler : IRequestHandler<CircleGoalAddCommand>
{

    private readonly CirclesDbContext _dbContext;

    public CircleGoalAddCommandHandler(CirclesDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task Handle(CircleGoalAddCommand request, CancellationToken cancellationToken)
    {
        var circle = await _dbContext.Circles.FirstOrDefaultAsync(e => e.Id == request.Params.CircleId, cancellationToken: cancellationToken);
        if (circle is null)
            throw new ItemCantBeFoundException("Circle");

        var item = CircleGoal.Create(circle, request.Params.Denomination);

        await _dbContext.AddAsync(item, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
