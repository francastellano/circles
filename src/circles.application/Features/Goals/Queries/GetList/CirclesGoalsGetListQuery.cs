using circles.api.contracts.Goals.Queries;
using circles.infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace circles.application.Features.Members.Queries.GetList;

public sealed record CirclesGoalsGetListQuery(Guid Id) : IRequest<List<CircleGoalsGetListResult>>;

internal sealed record CirclesGoalsGetListQueryHandler : IRequestHandler<CirclesGoalsGetListQuery, List<CircleGoalsGetListResult>>
{

    private readonly CirclesDbContext _context;

    public CirclesGoalsGetListQueryHandler(CirclesDbContext dbContext)
    {
        _context = dbContext;
    }


    public async Task<List<CircleGoalsGetListResult>> Handle(CirclesGoalsGetListQuery request, CancellationToken cancellationToken)
    {
        var baseQuery = _context.CircleGoals
                .Where(e => e.Circle.Id == request.Id)
                .OrderBy(e => e.Denomination)
                .AsQueryable();

        var query = baseQuery.Select(
            e => new CircleGoalsGetListResult(
                e.Id,
                e.Denomination
            )
        );

        var result = await query.ToListAsync(cancellationToken);
        return result;

    }
}
