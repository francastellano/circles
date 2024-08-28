using circles.api.contracts.Circles.Queries.GetById;
using circles.application.Abstractions.Messages;
using circles.application.Exceptions;
using circles.domain.Abstractions;
using circles.infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace circles.application.Features.Circles.Queries.GetList;

public sealed record CirclesGetByIdQuery(Guid Id) : IQuery<CircleGetByIdResult>;

internal sealed record CirclesGetByIdQueryHandler : IQueryHandler<CirclesGetByIdQuery, CircleGetByIdResult>
{

    private readonly CirclesDbContext _context;

    public CirclesGetByIdQueryHandler(CirclesDbContext dbContext)
    {
        _context = dbContext;
    }


    public async Task<Result<CircleGetByIdResult>> Handle(CirclesGetByIdQuery request, CancellationToken cancellationToken)
    {

        var circle = await _context.Circles.FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

        if (circle is null)
            throw new ItemCantBeFoundException("circle");

        var result = new CircleGetByIdResult(circle.Id, circle.Denomination, circle.Creator);

        return result;
    }
}