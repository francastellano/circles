
using circles.api.contracts.Members;
using circles.application.Features.Members.Commands.Add;
using FastEndpoints;
using MediatR;

namespace circles.api.EndPoints.Members;
public class CircleMemberAddEndPoint(IMediator mediator) : Endpoint<CircleMemberAddParams, Guid>
{
    public override void Configure()
    {
        Verbs(Http.POST);
        Routes("/api/v1/members");
    }

    public override async Task HandleAsync(CircleMemberAddParams req, CancellationToken ct)
    {
        await mediator.Send(new MembersAddCommand(req), ct);
    }
}
