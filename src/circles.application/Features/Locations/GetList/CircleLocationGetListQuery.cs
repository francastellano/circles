using circles.api.contracts.ActivityLocation.Queries;
using circles.application.Abstractions.Messages;
using circles.domain.Abstractions;
using circles.infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace circles.application.Features.Locations.GetList;

public sealed record CircleLocationGetListQuery(Guid CircleId) : IQuery<List<CircleLocationGetListResult>>;

internal sealed record CircleLocationGetListQueryHandler : IQueryHandler<CircleLocationGetListQuery, List<CircleLocationGetListResult>>
{

    private readonly CirclesDbContext _circleDbContext;
    public CircleLocationGetListQueryHandler(CirclesDbContext dbContext)
    {
        _circleDbContext = dbContext;
    }
    public async Task<Result<List<CircleLocationGetListResult>>> Handle(CircleLocationGetListQuery request, CancellationToken cancellationToken)
    {
        var result = await _circleDbContext.CircleLocations
                .Where(e => e.Circle.Id == request.CircleId)
                .Select(e => new CircleLocationGetListResult(e.Id, e.Denomination, e.Longitude, e.Latitude))
                .ToListAsync(cancellationToken);

        return result;
    }
}
