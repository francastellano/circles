using circles.api.contracts.Circles.Queries.GetList;
using circles.infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace circles.application.Features.Circles.GetList;

public sealed record CirclesGetListQuery(CircleGetListParams Param) : IRequest<List<CircleGetListResults>>;

internal sealed record CirclesGetListQueryHandler : IRequestHandler<CirclesGetListQuery, List<CircleGetListResults>>
{

    private readonly CirclesDbContext _context;

    public CirclesGetListQueryHandler(CirclesDbContext dbContext)
    {
        _context = dbContext;
    }


    public async Task<List<CircleGetListResults>> Handle(CirclesGetListQuery request, CancellationToken cancellationToken)
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