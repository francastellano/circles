using circles.api.contracts.Circles.Commands.Add;
using circles.application.Abstractions.Messages;
using circles.domain.Abstractions;
using circles.domain.Circles;
using circles.infrastructure.Context;
using FluentValidation;
using MediatR;

namespace circles.application.Features.Circles.Commands.Add;
public sealed record CirclesAddCommand(CircleAddRequest Parameter, string Email) : ICommand;

internal sealed record CirclesAddCommandHandler : ICommandHandler<CirclesAddCommand>
{
    private readonly CirclesDbContext _dbContext;
    private readonly IValidator<CircleAddRequest> _validator;

    public CirclesAddCommandHandler(CirclesDbContext dbContext, IValidator<CircleAddRequest> validator)
    {
        _dbContext = dbContext;
        _validator = validator;

    }
    public async Task<Result> Handle(CirclesAddCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request.Parameter, cancellationToken);

        if (!validationResult.IsValid)
        {
            return Result.Failure(new Error("100", validationResult.Errors[0].ErrorMessage));
        }

        var data = Circle.Create(request.Parameter.Denomination, request.Email);

        await _dbContext.Circles.AddAsync(data, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}