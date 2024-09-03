using circles.application.Exceptions;
using circles.domain.Circles.Events;
using circles.domain.Members;
using circles.infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace circles.application.Features.Circles.Events;

public class CircleCreateOwnerEvent : INotificationHandler<CircleCreatedEvent>
{
    private readonly CirclesDbContext _dbContext;

    public CircleCreateOwnerEvent(CirclesDbContext DbContext)
    {
        _dbContext = DbContext;
    }

    public async Task Handle(CircleCreatedEvent notification, CancellationToken cancellationToken)
    {

        var circle = await _dbContext.Circles.FirstOrDefaultAsync(e => e.Id == notification.Id, cancellationToken: cancellationToken);

        if (circle is null)
            throw new ItemCantBeFoundException($"Circle with Id {notification.Id}");

        var data = CircleMember.Create(circle, circle.Creator, circle.Creator);

        await _dbContext.CircleMembers.AddAsync(data, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}