using circles.api.contracts.Skills.Queries;
using circles.application.Features.Skills.Queries;
using FastEndpoints;
using MediatR;

namespace circles.api.EndPoints.Skills;

public class CircleSkillsGetListEndPoint(IMediator mediator) : Endpoint<CircleSkillGetListRequest, List<CircleSkillGetListResult>>
{
    public override void Configure()
    {
        Verbs(Http.GET);
        Routes("/api/v1/circles/{Id}/skills");
        Options(o => o.WithTags("Skills Management"));
    }

    public override async Task HandleAsync(CircleSkillGetListRequest req, CancellationToken ct)
    {
        var result = await mediator.Send(new CirclesSkillsGetListQuery(req.CircleId), ct);
        if (result.IsSuccess)
            await SendAsync(result.Value, cancellation: ct);
        else
            await SendErrorsAsync(cancellation: ct);
    }
}
