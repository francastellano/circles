using circles.api.contracts.Activities.Queries;
using circles.application.Abstractions.Messages;
using circles.application.Abstractions.Results;
using circles.domain.Abstractions;
using circles.infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace circles.application.Features.Activities.Queries.GetList;

public sealed record ActivityGetListQuery(ActivityGetListRequest Params) : IQuery<List<ActivityGetListResults>>;

internal sealed record ActivityGetListQueryHandler : IQueryHandler<ActivityGetListQuery, List<ActivityGetListResults>>
{

    private readonly CirclesDbContext _context;
    private readonly ILogger<ActivityGetListQueryHandler> _logger;

    public ActivityGetListQueryHandler(CirclesDbContext dbContext, ILogger<ActivityGetListQueryHandler> logger)
    {
        _context = dbContext;
        _logger = logger;
    }

    public async Task<Result<List<ActivityGetListResults>>> Handle(ActivityGetListQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var baseQuery = _context.CircleActivities
                    .Include(e => e.Location)
                    .Where(activity => activity.Circle.Id == request.Params.CircleId)
                    .OrderBy(activity => activity.Denomination)
                    .AsQueryable();

            var query = baseQuery.Select(
                e => new ActivityGetListResults(
                    e.Id,
                    e.Denomination,
                    e.Location.Denomination
                )
            );

            var result = await query.ToListAsync(cancellationToken);
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError("Failed to retrieve activities for circle {CircleId}. Exception: {ExceptionMessage}\nStackTrace: {StackTrace}",
                            request.Params.CircleId, e.Message, e.StackTrace);
            return ResultEntity<List<ActivityGetListResults>>.NonControledError();
        }

    }
}
