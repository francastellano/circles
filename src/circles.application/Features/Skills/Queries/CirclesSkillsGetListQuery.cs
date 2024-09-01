using circles.api.contracts.Skills.Queries;
using circles.application.Abstractions.Messages;
using circles.domain.Abstractions;
using circles.infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace circles.application.Features.Skills.Queries;

public sealed record CirclesSkillsGetListQuery(Guid Id) : IQuery<List<CircleSkillGetListResult>>;

internal sealed record CirclesSkillsGetListQueryHandler : IQueryHandler<CirclesSkillsGetListQuery, List<CircleSkillGetListResult>>
{

    private readonly CirclesDbContext _context;

    public CirclesSkillsGetListQueryHandler(CirclesDbContext dbContext)
    {
        _context = dbContext;
    }

    public async Task<Result<List<CircleSkillGetListResult>>> Handle(CirclesSkillsGetListQuery request, CancellationToken cancellationToken)
    {
        Console.WriteLine(request.Id);
        var baseQuery = _context.CircleSkills
                .Where(e => e.Circle.Id == request.Id)
                .OrderBy(e => e.Denomination)
                .AsQueryable();

        var query = baseQuery.Select(
            e => new CircleSkillGetListResult(
                e.Id,
                e.Denomination,
                e.Skill != null ? e.Skill.Id : null,
                e.Description
            )
        );

        var result = await query.ToListAsync(cancellationToken);

        return result;
    }
}