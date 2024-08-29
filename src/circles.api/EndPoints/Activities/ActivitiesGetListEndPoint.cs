using circles.api.contracts.Activities.Queries;
using circles.application.Features.Activities.Queries.GetList;
using FastEndpoints;
using MediatR;

namespace circles.api.EndPoints.Activities;

public class ActivitiesGetListEndPoint(IMediator mediator) : Endpoint<ActivityGetListRequest, List<ActivityGetListResults>>
{
    public override void Configure()
    {
        Verbs(Http.GET);
        Routes("/api/v1/circles/{Id}/activities");
        Options(o => o.WithTags("Activities Management"));
    }

    public override async Task HandleAsync(ActivityGetListRequest req, CancellationToken ct)
    {
        var result = await mediator.Send(new ActivityGetListQuery(req), ct);
        if (result.IsSuccess)
            await SendAsync(result.Value, cancellation: ct);
        else
            await SendErrorsAsync(cancellation: ct);
    }
}
