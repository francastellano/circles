using circles.api.contracts.Skills.Commands.Add;
using circles.application.Abstractions.Messages;
using circles.application.Exceptions;
using circles.domain.Abstractions;
using circles.domain.Skills;
using circles.infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace circles.application.Features.Skills.Commands;

public sealed record CircleSkillAddCommand(CircleSkillAddRequest Params) : ICommand;


internal sealed record CircleSkillAddCommandHandler(CirclesDbContext DbContext) : ICommandHandler<CircleSkillAddCommand>
{
    public async Task<Result> Handle(CircleSkillAddCommand request, CancellationToken cancellationToken)
    {
        var circle = await DbContext.Circles.FirstOrDefaultAsync(e => e.Id == request.Params.CircleId, cancellationToken: cancellationToken);
        if (circle is null)
            throw new ItemCantBeFoundException("Circle");

        var item = CircleSkill.Create(circle, request.Params.Denomination);

        await DbContext.AddAsync(item, cancellationToken);
        await DbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}