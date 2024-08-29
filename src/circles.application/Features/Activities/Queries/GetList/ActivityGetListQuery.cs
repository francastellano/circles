using circles.api.contracts.Activities.Queries;
using circles.application.Abstractions.Messages;
using circles.domain.Abstractions;
using circles.infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace circles.application.Features.Activities.Queries.GetList;

public sealed record ActivityGetListQuery(ActivityGetListRequest Params) : IQuery<List<ActivityGetListResults>>;

internal sealed record ActivityGetListQueryHandler : IQueryHandler<ActivityGetListQuery, List<ActivityGetListResults>>
{

    private readonly CirclesDbContext _context;

    public ActivityGetListQueryHandler(CirclesDbContext dbContext)
    {
        _context = dbContext;
    }


    public async Task<Result<List<ActivityGetListResults>>> Handle(ActivityGetListQuery request, CancellationToken cancellationToken)
    {
        var baseQuery = _context.CircleActivities
                .Where(e => e.Circle.Id == request.Params.CircleId)
                .OrderBy(e => e.Denomination)
                .AsQueryable();

        var query = baseQuery.Select(
            e => new ActivityGetListResults(
                e.Id,
                e.Denomination
            )
        );

        var result = await query.ToListAsync(cancellationToken);
        return result;

    }
}
