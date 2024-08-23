using circles.api.contracts.Members.Queries;
using circles.application.Features.Members.Queries.GetList;
using FastEndpoints;
using MediatR;

namespace circles.api.EndPoints.Members;

public class CircleGetMembersEndPoint(IMediator mediator) : Endpoint<CircleLitGetMemberParams, List<CircleListGetMemberResult>>
{
    public override void Configure()
    {
        Verbs(Http.GET);
        Routes("/api/v1/circles/{Id}/members");
    }

    public override async Task HandleAsync(CircleLitGetMemberParams req, CancellationToken ct)
    {
        var result = await mediator.Send(new CirclesMembersGetListQuery(req.Id), ct);
        await SendAsync(result);
    }
}
