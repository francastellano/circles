using circles.api.contracts.Goals.Commands;
using circles.application.Features.Goals.Commands;
using FastEndpoints;
using MediatR;

namespace circles.api.EndPoints.Skills;

public class CircleGoalsAddEndPoint(IMediator mediator) : Endpoint<CircleGoalAddRequest, Guid>
{
    public override void Configure()
    {
        Verbs(Http.POST);
        Routes("/api/v1/goals");
        Options(o => o.WithTags("Goals Management"));
    }

    public override async Task HandleAsync(CircleGoalAddRequest req, CancellationToken ct)
    {
        await mediator.Send(new CircleGoalAddCommand(req), ct);
    }
}