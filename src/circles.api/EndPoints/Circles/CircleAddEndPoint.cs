using circles.api.contracts.Circles.Commands.Add;
using circles.application.Exception;
using circles.application.Features.Circles.Commands.Add;
using FastEndpoints;
using MediatR;

namespace circles.api.EndPoints;

public class CircleAddEndPoint : Endpoint<CircleAddRequest, Guid>
{

    private readonly IMediator _mediator;

    public CircleAddEndPoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Verbs(Http.POST);
        Routes("/api/v1/circles");
        Options(o => o.WithTags("Circles Management"));
    }

    public override async Task HandleAsync(CircleAddRequest req, CancellationToken ct)
    {
        var email = User.Claims.FirstOrDefault(e => e.Type == "emails")?.Value;
        if (string.IsNullOrEmpty(email))
            throw new UserCantBeFoundException();

        var result = await _mediator.Send(new CirclesAddCommand(req, email), ct);
        if (result.IsSuccess)
            await SendOkAsync(ct);
        else
            await SendErrorsAsync(cancellation: ct);
    }
}
