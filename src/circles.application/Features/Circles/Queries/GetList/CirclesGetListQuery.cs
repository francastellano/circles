using circles.api.contracts.Circles.Queries.GetList;
using circles.application.Abstractions.Messages;
using circles.domain.Abstractions;
using circles.infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace circles.application.Features.Circles.Queries.GetList;

public sealed record CirclesGetListQuery(CircleGetListRequest Param) : IQuery<List<CircleGetListResults>>;

internal sealed record CirclesGetListQueryHandler : IQueryHandler<CirclesGetListQuery, List<CircleGetListResults>>
{

    private readonly CirclesDbContext _context;

    public CirclesGetListQueryHandler(CirclesDbContext dbContext)
    {
        _context = dbContext;
    }


    public async Task<Result<List<CircleGetListResults>>> Handle(CirclesGetListQuery request, CancellationToken cancellationToken)
    {

        var baseQuery = _context.Circles
                .OrderBy(e => e.Denomination)
                .AsQueryable();

        if (!string.IsNullOrEmpty(request.Param.Denomination))
            baseQuery = baseQuery.Where(e => e.Denomination.Contains(request.Param.Denomination));

        var query = baseQuery.Select(
            e => new CircleGetListResults(
                e.Id,
                e.Denomination
            )
        );

        return await query.ToListAsync(cancellationToken);
    }
}