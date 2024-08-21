using FastEndpoints;
using MediatR;
using circles.api.contracts.Circles.Queries.GetList;
using circles.application.Features.Circles.GetList;

namespace circles.api.EndPoints.Circles;

public class CircleGetListEndPoint(IMediator mediator) : Endpoint<CircleGetListParams, List<CircleGetListResults>>
{
    public override void Configure()
    {
        Verbs(Http.GET);
        Routes("/api/v1/circles");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CircleGetListParams req, CancellationToken ct)
    {

        var result = await mediator.Send(new CirclesGetListQuery(req), ct);
        await SendAsync(result);
    }
}
