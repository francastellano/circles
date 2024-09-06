using circles.api.contracts.Skills.Commands.Add;
using circles.application.Abstractions.Messages;
using circles.application.Exceptions;
using circles.domain.Abstractions;
using circles.domain.Skills;
using circles.infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace circles.application.Features.Skills.Commands;

public sealed record CircleSkillAddCommand(CircleSkillAddRequest Params) : ICommand;

internal sealed record CircleSkillAddCommandHandler : ICommandHandler<CircleSkillAddCommand>
{
    private readonly CirclesDbContext _dbContext;
    public CircleSkillAddCommandHandler(CirclesDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Result> Handle(CircleSkillAddCommand request, CancellationToken cancellationToken)
    {
        var circle = await _dbContext.Circles.FirstOrDefaultAsync(e => e.Id == request.Params.CircleId, cancellationToken: cancellationToken);
        if (circle is null)
            throw new ItemCantBeFoundException("Circle");

        CircleSkill? mainSkill = null;
        if (request.Params.SkillId != null)
            mainSkill = await _dbContext.CircleSkills.FirstOrDefaultAsync(e => e.Id == request.Params.SkillId, cancellationToken);

        var item = CircleSkill.Create(circle, request.Params.Denomination, mainSkill, request.Params.Description);

        await _dbContext.AddAsync(item, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}