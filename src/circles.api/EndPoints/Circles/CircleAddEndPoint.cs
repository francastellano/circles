using circles.api.contracts.Circles.Commands.Add;
using circles.application.Exception;
using circles.application.Features.Circles.Commands.Add;
using FastEndpoints;
using MediatR;

namespace circles.api.EndPoints;

public class CircleAddEndPoint(IMediator mediator) : Endpoint<CircleAddRequest, Guid>
{
    public override void Configure()
    {
        Verbs(Http.POST);
        Routes("/api/v1/circles");
        Options(o => o.WithTags("Circles Management"));
    }

    public override async Task HandleAsync(CircleAddRequest req, CancellationToken ct)
    {
        var email = User.Claims.FirstOrDefault(e => e.Type == "emails");
        if (email is null)
            throw new UserCantBeFoundException();

        await mediator.Send(new CirclesAddCommand(req, email.Value), ct);
    }
}
