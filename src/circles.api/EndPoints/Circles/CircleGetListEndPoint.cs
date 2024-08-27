using FastEndpoints;
using MediatR;
using circles.api.contracts.Circles.Queries.GetList;
using circles.application.Features.Circles.Queries.GetList;

namespace circles.api.EndPoints.Circles;

public class CircleGetListEndPoint(IMediator mediator) : Endpoint<CircleGetListRequest, List<CircleGetListResults>>
{
    public override void Configure()
    {
        Verbs(Http.GET);
        Routes("/api/v1/circles");
        Options(o => o.WithTags("Circles Management"));
    }

    public override async Task HandleAsync(CircleGetListRequest req, CancellationToken ct)
    {
        var result = await mediator.Send(new CirclesGetListQuery(req), ct);
        await SendAsync(result);
    }
}
