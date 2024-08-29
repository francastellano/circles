using circles.api.contracts.Members.Queries;
using circles.application.Features.Members.Queries.GetById;
using FastEndpoints;
using MediatR;

namespace circles.api.EndPoints.Members;
public class CircleMembersGetByIdEndPoint(IMediator mediator) : Endpoint<CircleMemberGetByIdRequest, CircleMemberGetByIdResponse>
{
    public override void Configure()
    {
        Verbs(Http.GET);
        Routes("/api/v1/members/{Id}");
        Options(o => o.WithTags("Members Management"));
    }

    public override async Task HandleAsync(CircleMemberGetByIdRequest req, CancellationToken ct)
    {
        var result = await mediator.Send(new CircleMembersGetByIdQuery(req.Id), ct);

        if (result.IsSuccess)
            await SendAsync(result.Value, cancellation: ct);
        else
            await SendErrorsAsync(cancellation: ct);
    }
}
