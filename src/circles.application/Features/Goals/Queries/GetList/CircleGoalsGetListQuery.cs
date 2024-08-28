using circles.api.contracts.Goals.Queries;
using circles.application.Abstractions.Messages;
using circles.domain.Abstractions;
using circles.infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace circles.application.Features.Members.Queries.GetList;

public sealed record CircleGoalsGetListQuery(Guid Id) : IQuery<List<CircleGoalsGetListResult>>;

internal sealed record CircleGoalsGetListQueryHandler : IQueryHandler<CircleGoalsGetListQuery, List<CircleGoalsGetListResult>>
{

    private readonly CirclesDbContext _context;

    public CircleGoalsGetListQueryHandler(CirclesDbContext dbContext)
    {
        _context = dbContext;
    }


    public async Task<Result<List<CircleGoalsGetListResult>>> Handle(CircleGoalsGetListQuery request, CancellationToken cancellationToken)
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
