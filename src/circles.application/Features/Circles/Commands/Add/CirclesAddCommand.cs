using circles.api.contracts.Circles.Commands.Add;
using circles.domain.Circles;
using circles.infrastructure.Context;
using FluentValidation;
using MediatR;

namespace circles.application.Features.Circles.Commands.Add;
public sealed record CirclesAddCommand(CircleAddRequest Parameter, string Email) : IRequest;

internal sealed record CirclesAddCommandHandler : IRequestHandler<CirclesAddCommand>
{
    private readonly CirclesDbContext DbContext;
    private readonly IValidator<CircleAddRequest> _validator;

    public CirclesAddCommandHandler(CirclesDbContext dbContext, IValidator<CircleAddRequest> validator)
    {
        DbContext = dbContext;
        _validator = validator;

    }
    public async Task Handle(CirclesAddCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request.Parameter, cancellationToken);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var data = Circle.Create(request.Parameter.Denomination, request.Email);

        await DbContext.Circles.AddAsync(data, cancellationToken);
        await DbContext.SaveChangesAsync(cancellationToken);
    }
}