using circles.api.contracts.Members.Queries;
using circles.application.Abstractions.Messages;
using circles.application.Abstractions.Pagination;
using circles.domain.Abstractions;
using circles.infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace circles.application.Features.Members.Queries.GetList;

public sealed record CirclesMembersGetListQuery(CircleGetListMembersRequest Params) : IQuery<PagingResult<CircleListGetMemberResult>>;

internal sealed record CirclesMembersGetListQueryHandler : IQueryHandler<CirclesMembersGetListQuery, PagingResult<CircleListGetMemberResult>>
{

    private readonly CirclesDbContext _context;

    public CirclesMembersGetListQueryHandler(CirclesDbContext dbContext)
    {
        _context = dbContext;
    }


    public async Task<Result<PagingResult<CircleListGetMemberResult>>> Handle(CirclesMembersGetListQuery request, CancellationToken cancellationToken)
    {
        var source = _context.CircleMembers
                //.Where(e => e.Circle.Id == request.Params.CircleId)
                .OrderBy(e => e.Email)
                .Select(e => new CircleListGetMemberResult
            (
               e.Id,
                e.Circle.Id,
                e.Email,
                e.Name
            ))
            .AsNoTracking()
            .AsQueryable();

        var pagination = new Pagination<CircleListGetMemberResult>();

        var result = await pagination.FetchPaginatedDataAsync(source, request.Params.PageNumber, request.Params.PageSize);

        return result;
    }

}

