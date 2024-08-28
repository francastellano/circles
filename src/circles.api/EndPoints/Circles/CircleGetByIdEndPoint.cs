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
        Options(o => o.WithTags("Circles Management"));
    }

    public override async Task HandleAsync(CircleGetByIdRequest req, CancellationToken ct)
    {
        var result = await mediator.Send(new CirclesGetByIdQuery(req.Id), ct);
        if (result.IsSuccess)
            await SendAsync(result.Value, cancellation: ct);
        else
            await SendErrorsAsync(cancellation: ct);
    }
}

