using circles.api.contracts.Members;
using circles.application.Exceptions;
using circles.domain.Members;
using circles.infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace circles.application.Features.Members.Commands.Add;

public sealed record MembersAddCommand(CircleMemberAddParams Params) : IRequest;

internal sealed record MembersAddCommandHandler(CirclesDbContext DbContext) : IRequestHandler<MembersAddCommand>
{
    public async Task Handle(MembersAddCommand request, CancellationToken cancellationToken)
    {
        var circle = await DbContext.Circles.FirstOrDefaultAsync(e => e.Id == request.Params.CircleId, cancellationToken: cancellationToken);
        if (circle is null)
            throw new ItemCantBeFoundException("Circle");

        var circleMember = CircleMember.Create(circle, request.Params.Email);

        await DbContext.AddAsync(circleMember, cancellationToken);
        await DbContext.SaveChangesAsync(cancellationToken);


    }
}