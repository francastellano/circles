using circles.api.contracts.Members.Queries;
using circles.application.Exceptions;
using circles.infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace circles.application.Features.Members.Queries.GetById;

public sealed record CircleMembersGetByIdQuery(Guid Id) : IRequest<CircleMemberGetByIdResponse>;

internal sealed record CircleMembersGetByIdQueryHandler : IRequestHandler<CircleMembersGetByIdQuery, CircleMemberGetByIdResponse>
{

    private readonly CirclesDbContext _circlesDbContext;
    public CircleMembersGetByIdQueryHandler(CirclesDbContext circlesDbContext)
    {
        _circlesDbContext = circlesDbContext;
    }


    public async Task<CircleMemberGetByIdResponse> Handle(CircleMembersGetByIdQuery request, CancellationToken cancellationToken)
    {
        var data = await _circlesDbContext.CircleMembers.FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

        if (data is null)
            throw new ItemCantBeFoundException("Circle Member");

        var response = new CircleMemberGetByIdResponse(data.Id, data.Email);
        return response;
    }
}