using circles.api.contracts.Circles.Commands.Add;
using circles.domain.Circles;
using circles.infrastructure.Context;
using MediatR;

namespace circles.application.Features.Circles.Commands.Add;
public sealed record CirclesAddCommand(CircleAddParams parameter) : IRequest;

internal sealed record CirclesAddCommandHandler : IRequestHandler<CirclesAddCommand>
{
    private readonly CirclesDbContext DbContext;

    public CirclesAddCommandHandler(CirclesDbContext dbContext)
    {
        DbContext = dbContext;

    }
    public async Task Handle(CirclesAddCommand request, CancellationToken cancellationToken)
    {
        var data = new Circle(request.parameter.Denomination);

        await DbContext.Circles.AddAsync(data, cancellationToken);
        await DbContext.SaveChangesAsync(cancellationToken);
    }
}