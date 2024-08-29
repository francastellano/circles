using circles.api.contracts.Activities.Commands;
using circles.application.Features.Activities.Commands.Add;
using FastEndpoints;
using MediatR;

namespace circles.api.EndPoints.Activities;
public class ActivitiesAddEndPoint : Endpoint<CircleActivityAddRequest, Guid>
{
    private readonly IMediator _mediator;

    public ActivitiesAddEndPoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Verbs(Http.POST);
        Routes("/api/v1/activities");
        Options(o => o.WithTags("Activities Management"));
    }

    public override async Task HandleAsync(CircleActivityAddRequest req, CancellationToken ct)
    {
        var result = await _mediator.Send(new CirclesActivityAddCommand(req), ct);

        if (result.IsSuccess)
            await SendOkAsync(ct);
        else
            await SendErrorsAsync(cancellation: ct);
    }
}
