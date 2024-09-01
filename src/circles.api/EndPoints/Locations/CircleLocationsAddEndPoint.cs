using circles.api.contracts.CircleLocation.Commands;
using circles.application.Features.Locations.Commands;
using circles.application.Features.Locations.Commands.Add;
using FastEndpoints;
using MediatR;

namespace circles.api.EndPoints.Locations;

public class CircleLocationsAddEndPoint (IMediator mediator) : Endpoint<CircleLocationAddRequest, Guid>
{
    public override void Configure()
    {
        Verbs(Http.POST);
        Routes("/api/v1/locations");
        Options(o => o.WithTags("Location Management"));
    }

    public override async Task HandleAsync(CircleLocationAddRequest req, CancellationToken ct)
    {
        await mediator.Send(new CircleLocationAddCommand(req), ct);
    }
}
