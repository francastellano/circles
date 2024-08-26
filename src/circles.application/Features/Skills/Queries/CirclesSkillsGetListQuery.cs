using circles.api.contracts.Skills.Queries;
using circles.infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace circles.application.Features.Skills.Queries;

public sealed record CirclesSkillsGetListQuery(Guid Id) : IRequest<List<CircleSkillGetListResult>>;

internal sealed record CirclesSkillsGetListQueryHandler : IRequestHandler<CirclesSkillsGetListQuery, List<CircleSkillGetListResult>>
{

    private readonly CirclesDbContext _context;

    public CirclesSkillsGetListQueryHandler(CirclesDbContext dbContext)
    {
        _context = dbContext;
    }

    public async Task<List<CircleSkillGetListResult>> Handle(CirclesSkillsGetListQuery request, CancellationToken cancellationToken)
    {
        Console.WriteLine(request.Id);
        var baseQuery = _context.CircleSkills
                .Where(e => e.Circle.Id == request.Id)
                .OrderBy(e => e.Denomination)
                .AsQueryable();

        var query = baseQuery.Select(
            e => new CircleSkillGetListResult(
                e.Id,
                e.Denomination
            )
        );

        var result = await query.ToListAsync(cancellationToken);

        return result;
    }
}