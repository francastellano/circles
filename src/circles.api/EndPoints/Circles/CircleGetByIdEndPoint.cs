using circles.api.contracts.Circles.Queries.GetById;
using circles.application.Features.Circles.Queries.GetList;
using FastEndpoints;
using MediatR;

namespace circles.api.EndPoints.Circles;

public class CircleGetByIdEndPoint(IMediator mediator) : EndpointWithoutRequest<CircleGetByIdResult>
{
    public override void Configure()
    {
        Verbs(Http.GET);
        Routes("/api/v1/circles/{Id}");
        Options(o => o.WithTags("Circles Management"));
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var id = Route<Guid>("Id");
        var result = await mediator.Send(new CirclesGetByIdQuery(new CircleGetByIdRequest(id)), ct);
        if (result.IsSuccess)
            await SendAsync(result.Value, cancellation: ct);
        else
            await SendErrorsAsync(cancellation: ct);
    }
}

