using circles.api.contracts.Circles.Commands.Add;
using circles.domain.Circles;
using circles.infrastructure.Context;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore.Query;

namespace circles.application.Features.Circles.Commands.Add;
public sealed record CirclesAddCommand(CircleAddParams Parameter) : IRequest;

internal sealed record CirclesAddCommandHandler : IRequestHandler<CirclesAddCommand>
{
    private readonly CirclesDbContext DbContext;
    private readonly IValidator<CircleAddParams> _validator;

    public CirclesAddCommandHandler(CirclesDbContext dbContext, IValidator<CircleAddParams> validator)
    {
        DbContext = dbContext;
        _validator = validator;

    }
    public async Task Handle(CirclesAddCommand request, CancellationToken cancellationToken)
    {

        // Validate the request parameters
        var validationResult = await _validator.ValidateAsync(request.Parameter, cancellationToken);

        if (!validationResult.IsValid)
        {

            // You can handle validation failures here (e.g., throw an exception or return an error response)
            throw new ValidationException(validationResult.Errors);

        }

        var data = Circle.Create(request.Parameter.Denomination);

        await DbContext.Circles.AddAsync(data, cancellationToken);
        await DbContext.SaveChangesAsync(cancellationToken);
    }
}