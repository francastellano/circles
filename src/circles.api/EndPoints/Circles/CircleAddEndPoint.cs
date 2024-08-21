using circles.api.contracts.Circles.Commands.Add;
using circles.api.contracts.Members;
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
        AllowAnonymous();
    }

    public override async Task HandleAsync(CircleAddParams req, CancellationToken ct)
    {

        await mediator.Send(new CirclesAddCommand(req), ct);

        //await SendAsync(response);
    }
}
