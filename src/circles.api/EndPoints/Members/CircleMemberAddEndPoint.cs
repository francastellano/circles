
using circles.api.contracts.Members.Commands;
using circles.application.Features.Members.Commands.Add;
using FastEndpoints;
using MediatR;

namespace circles.api.EndPoints.Members;
public class CircleMemberAddEndPoint(IMediator mediator) : Endpoint<CircleMemberAddRequest, Guid>
{
    public override void Configure()
    {
        Verbs(Http.POST);
        Routes("/api/v1/members");
        Options(o => o.WithTags("Members Management"));
    }

    public override async Task HandleAsync(CircleMemberAddRequest req, CancellationToken ct)
    {
        await mediator.Send(new MembersAddCommand(req), ct);
    }
}
