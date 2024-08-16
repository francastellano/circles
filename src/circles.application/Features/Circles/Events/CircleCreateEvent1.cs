using circles.domain.Circles.Events;
using MediatR;

namespace circles.application.Features.Circles.Events;

public class CircleCreateEvent1 : INotificationHandler<CircleCreatedEvent>
{
    public Task Handle(CircleCreatedEvent notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Event 1 {notification.Id}");
        return Task.CompletedTask;
    }
}