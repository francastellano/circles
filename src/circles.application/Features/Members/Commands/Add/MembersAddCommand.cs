using circles.api.contracts.Members.Commands;
using circles.application.Abstractions.Messages;
using circles.application.Exceptions;
using circles.domain.Abstractions;
using circles.domain.Members;
using circles.infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace circles.application.Features.Members.Commands.Add;

public sealed record MembersAddCommand(CircleMemberAddRequest Params) : ICommand;

internal sealed record MembersAddCommandHandler(CirclesDbContext DbContext) : ICommandHandler<MembersAddCommand>
{
    public async Task<Result> Handle(MembersAddCommand request, CancellationToken cancellationToken)
    {
        var circle = await DbContext.Circles.FirstOrDefaultAsync(e => e.Id == request.Params.CircleId, cancellationToken: cancellationToken);
        if (circle is null)
            throw new ItemCantBeFoundException("Circle");

        var circleMember = CircleMember.Create(circle, request.Params.Email);

        await DbContext.AddAsync(circleMember, cancellationToken);
        await DbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();

    }
}