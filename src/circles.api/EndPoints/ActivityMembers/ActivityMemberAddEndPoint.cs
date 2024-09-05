using circles.api.contracts.ActivityMembers.Commands;
using circles.application.Features.ActivityMembers.Commands.Add;
using FastEndpoints;
using MediatR;

namespace circles.api.EndPoints.ActivityMembers;

public class ActivityMemberAddEndPoint : Endpoint<ActivityMemberAddRequest, Guid>
{
    private readonly IMediator _mediator;

    public ActivityMemberAddEndPoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Verbs(Http.POST);
        Routes("/api/v1/activitymembers");
        Options(o => o.WithTags("Activities Management"));
    }

    public override async Task HandleAsync(ActivityMemberAddRequest req, CancellationToken ct)
    {
        var result = await _mediator.Send(new ActivityMemberAddCommand(req), ct);

        if (result.IsSuccess)
            await SendOkAsync(ct);
        else
            await SendErrorsAsync(cancellation: ct);
    }
}
