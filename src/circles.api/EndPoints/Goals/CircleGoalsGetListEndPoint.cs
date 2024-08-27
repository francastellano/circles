using circles.api.contracts.Goals.Queries;
using circles.api.contracts.Skills.Queries;
using circles.application.Features.Members.Queries.GetList;
using circles.application.Features.Skills.Queries;
using FastEndpoints;
using MediatR;

namespace circles.api.EndPoints.Skills;

public class CircleGoalsGetListEndPoint(IMediator mediator) : Endpoint<CircleGoalsGetListRequest, List<CircleGoalsGetListResult>>
{
    public override void Configure()
    {
        Verbs(Http.GET);
        Routes("/api/v1/circles/{Id}/goals");
        Options(o => o.WithTags("Goals Management"));
    }

    public override async Task HandleAsync(CircleGoalsGetListRequest req, CancellationToken ct)
    {
        var result = await mediator.Send(new CircleGoalsGetListQuery(req.CircleId), ct);
        await SendAsync(result);
    }
}
