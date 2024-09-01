
using circles.api.contracts.CircleLocation.Commands;
using circles.application.Abstractions.Messages;
using circles.application.Exceptions;
using circles.domain.Abstractions;
using circles.domain.ActivityLocations;
using circles.infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace circles.application.Features.Locations.Commands.Add;

public sealed record CircleLocationAddCommand (CircleLocationAddRequest Params): ICommand;

internal sealed record CircleLocationAddCommandHandler : ICommandHandler<CircleLocationAddCommand>
{
    private readonly CirclesDbContext _dbContext;

    public CircleLocationAddCommandHandler(CirclesDbContext circleDbContext)
    {
        _dbContext = circleDbContext;
    }
    public async Task<Result> Handle(CircleLocationAddCommand request, CancellationToken cancellationToken)
    {

        var circle = await _dbContext.Circles.FirstOrDefaultAsync(e => e.Id == request.Params.CircleId, cancellationToken: cancellationToken);
        if (circle is null)
            throw new ItemCantBeFoundException("Circle");

        var item = CircleLocation.Create(circle, request.Params.Denomination, request.Params.latitude, request.Params.longitude);

        await _dbContext.AddAsync(item, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();

    }
}

