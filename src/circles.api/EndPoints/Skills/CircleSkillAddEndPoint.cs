using circles.api.contracts.Skills.Commands.Add;
using circles.application.Features.Skills.Commands;
using FastEndpoints;
using MediatR;

namespace circles.api.EndPoints.Skills;


public class CircleSkillAddEndPoint(IMediator mediator) : Endpoint<CircleSkillAddRequest, Guid>
{
    public override void Configure()
    {
        Verbs(Http.POST);
        Routes("/api/v1/skills");
        Options(o => o.WithTags("Skills Management"));
    }

    public override async Task HandleAsync(CircleSkillAddRequest req, CancellationToken ct)
    {
        await mediator.Send(new CircleSkillAddCommand(req), ct);
    }
}
