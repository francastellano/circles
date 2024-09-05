using circles.api.contracts.Activities.Queries;
using circles.application.Features.Activities.Queries.GetById;
using FastEndpoints;
using MediatR;

namespace circles.api.EndPoints.Activities;

public class ActivitiesGetByIdEndPoint(IMediator mediator) : EndpointWithoutRequest<ActivityGetByIdResults>
{
    public override void Configure()
    {
        Verbs(Http.GET);
        Routes("/api/v1/activities/{Id}");
        Options(o => o.WithTags("Activities Management"));
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var id = Route<Guid>("Id");
        var result = await mediator.Send(new ActivityGetByIdQuery(new ActivityGetByIdRequest(id)), ct);
        if (result.IsSuccess)
            await SendAsync(result.Value, cancellation: ct);
        else
            await SendErrorsAsync(cancellation: ct);
    }
}
