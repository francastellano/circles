using circles.api.contracts.Members.Commands;
using circles.application.Abstractions.Messages;
using circles.application.Exceptions;
using circles.domain.Abstractions;
using circles.domain.Members;
using circles.infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace circles.application.Features.Members.Commands.Add;

public sealed record MembersAddCommand(CircleMemberAddRequest Params) : ICommand;

internal sealed record MembersAddCommandHandler : ICommandHandler<MembersAddCommand>
{

    private readonly CirclesDbContext _dbContext;

    public MembersAddCommandHandler(CirclesDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Result> Handle(MembersAddCommand request, CancellationToken cancellationToken)
    {
        var circle = await _dbContext.Circles.FirstOrDefaultAsync(e => e.Id == request.Params.CircleId, cancellationToken: cancellationToken);
        if (circle is null)
            throw new ItemCantBeFoundException("Circle");

        var circleMember = CircleMember.Create(circle, request.Params.Email, request.Params.Name);

        await _dbContext.AddAsync(circleMember, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();

    }
}