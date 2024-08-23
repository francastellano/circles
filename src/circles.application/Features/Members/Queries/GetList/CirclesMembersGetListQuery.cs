using circles.api.contracts.Members.Queries;
using circles.infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace circles.application.Features.Members.Queries.GetList;

public sealed record CirclesMembersGetListQuery(Guid Id) : IRequest<List<CircleListGetMemberResult>>;

internal sealed record CirclesMembersGetListQueryHandler : IRequestHandler<CirclesMembersGetListQuery, List<CircleListGetMemberResult>>
{

    private readonly CirclesDbContext _context;

    public CirclesMembersGetListQueryHandler(CirclesDbContext dbContext)
    {
        _context = dbContext;
    }


    public async Task<List<CircleListGetMemberResult>> Handle(CirclesMembersGetListQuery request, CancellationToken cancellationToken)
    {
        var baseQuery = _context.CircleMembers
                .Where(e => e.Circle.Id == request.Id)
                .OrderBy(e => e.Email)
                .AsQueryable();

        var query = baseQuery.Select(
            e => new CircleListGetMemberResult(
                e.Id,
                e.Email
            )
        );

        var result = await query.ToListAsync(cancellationToken);
        return result;

    }
}
