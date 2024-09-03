using circles.api.contracts.Abstractions.Paginations;
using circles.api.contracts.Members.Queries;
using circles.application.Features.Members.Queries.GetList;
using FastEndpoints;
using MediatR;

namespace circles.api.EndPoints.Members;

public class CircleMembersGetListEndPoint(IMediator mediator) : Endpoint<CircleGetListMembersRequest, PagingResult<CircleListGetMemberResult>>
{
    public override void Configure()
    {
        Verbs(Http.GET);
        Routes("/api/v1/circles/{Id}/members");
        Options(o => o.WithTags("Members Management"));
    }

    public override async Task HandleAsync(CircleGetListMembersRequest req, CancellationToken ct)
    {
        var result = await mediator.Send(new CirclesMembersGetListQuery(req), ct);
        if (result.IsSuccess)
            await SendAsync(result.Value, cancellation: ct);
        else
            await SendErrorsAsync(cancellation: ct);
    }
}
