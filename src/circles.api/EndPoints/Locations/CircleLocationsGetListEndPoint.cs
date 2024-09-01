using circles.api.contracts.ActivityLocation.Queries;
using circles.api.contracts.CircleLocation.Queries;
using circles.api.contracts.Goals.Queries;
using circles.application.Features.Locations.GetList;
using FastEndpoints;
using MediatR;

namespace circles.api.EndPoints.Locations;

public class CircleLocationsGetListEndPoint (IMediator mediator) : 

Endpoint<CircleLocationGetListRequest, List<CircleLocationGetListResult>>
{
    public override void Configure()
    {
        Verbs(Http.GET);
        Routes("/api/v1/circles/{Id}/locations");
        Options(o => o.WithTags("Locations Management"));
    }

    public override async Task HandleAsync(CircleLocationGetListRequest req, CancellationToken ct)
    {
        var result = await mediator.Send(new CircleLocationGetListQuery(req.CircleId), ct);
        if (result.IsSuccess)
            await SendAsync(result.Value, cancellation: ct);
        else
            await SendErrorsAsync(cancellation: ct);
    }
}
