using circles.api.contracts.Activities.Commands;
using circles.application.Abstractions.Messages;
using circles.domain.Abstractions;
using circles.domain.Activities;
using circles.infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace circles.application.Features.Activities.Commands.Add;

public sealed record CirclesActivityAddCommand(CircleActivityAddRequest Params) : ICommand;

internal sealed record CirclesActivityAddCommandHandler : ICommandHandler<CirclesActivityAddCommand>
{
    private readonly CirclesDbContext _dbContext;

    public CirclesActivityAddCommandHandler(CirclesDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Result> Handle(CirclesActivityAddCommand request, CancellationToken cancellationToken)
    {
        var circle = await _dbContext.Circles.FirstOrDefaultAsync(e => e.Id == request.Params.CircleId, cancellationToken: cancellationToken);
        if (circle is null)
            return Result.Failure(new Error("100", "Circle can't be found"));

        var location = await _dbContext.CircleLocations.FirstOrDefaultAsync(e => e.Id == request.Params.LocationId, cancellationToken);
        if (location is null)
            return Result.Failure(new Error("100", "Location can't be found"));


        var item = CircleActivity.Create(circle, location, request.Params.Denomination, request.Params.ActivityInit.ToUniversalTime());

        await _dbContext.AddAsync(item, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
