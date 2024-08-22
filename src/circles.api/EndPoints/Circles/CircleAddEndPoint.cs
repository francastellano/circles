using circles.api.contracts.Circles.Commands.Add;
using circles.application.Features.Circles.Commands.Add;
using FastEndpoints;
using MediatR;

namespace circles.api.EndPoints;

public class CircleAddEndPoint(IMediator mediator) : Endpoint<CircleAddParams, Guid>
{
    public override void Configure()
    {
        Verbs(Http.POST);
        Routes("/api/v1/circles");
    }

    public override async Task HandleAsync(CircleAddParams req, CancellationToken ct)
    {
        var email = User.Claims.FirstOrDefault(e => e.Type == "emails");
        if (email is null)
            throw new NullReferenceException("Email is null");

        await mediator.Send(new CirclesAddCommand(req, email.Value), ct);
    }
}
