using circles.api.contracts.Activities.Queries;
using circles.application.Abstractions.Messages;
using circles.application.Exceptions;
using circles.infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using circles.domain.Abstractions;
using circles.domain.ActivityLocations;

namespace circles.application.Features.Activities.Queries.GetById;

public sealed record ActivityGetByIdQuery(ActivityGetByIdRequest Params) : IQuery<ActivityGetByIdResults>;

internal sealed record ActivityGetByIdQueryHandler : IQueryHandler<ActivityGetByIdQuery, ActivityGetByIdResults>
{

    private readonly CirclesDbContext _context;

    public ActivityGetByIdQueryHandler(CirclesDbContext dbContext)
    {
        _context = dbContext;
    }


    public async Task<Result<ActivityGetByIdResults>> Handle(ActivityGetByIdQuery request, CancellationToken cancellationToken)
    {

        var activity = await _context.CircleActivities
            .Include(e => e.Location)
            .FirstOrDefaultAsync(e => e.Id == request.Params.ActivityId, cancellationToken);

        if (activity is null)
            throw new ItemCantBeFoundException("activity");

        var members = await _context.ActivityMembers.Where(e => e.Activity.Id == activity.Id).Select(e => e.Member.Email).ToListAsync(cancellationToken);

        var result = new ActivityGetByIdResults(activity.Id, activity.Denomination, activity.ActivityInit, activity.Location.Denomination, activity.Location.Latitude, activity.Location.Longitude, members);

        return result;
    }
}