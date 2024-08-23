using circles.api.contracts.Circles.Queries.GetById;
using circles.application.Features.Circles.Queries.GetList;
using FastEndpoints;
using MediatR;

namespace circles.api.EndPoints.Circles;

public class CircleGetByIdEndPoint(IMediator mediator) : Endpoint<CircleGetByIdRequest, CircleGetByIdResult>
{
    public override void Configure()
    {
        Verbs(Http.GET);
        Routes("/api/v1/circles/{Id}");
    }

    public override async Task HandleAsync(CircleGetByIdRequest req, CancellationToken ct)
    {
        var result = await mediator.Send(new CirclesGetByIdQuery(req.Id), ct);
        await SendAsync(result, cancellation: ct);
    }
}

