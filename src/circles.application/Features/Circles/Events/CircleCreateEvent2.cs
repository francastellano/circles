using circles.domain.Circles.Events;
using MediatR;

namespace circles.application.Features.Circles.Events;

public class CircleCreateEvent2 : INotificationHandler<CircleCreatedEvent>
{
    public Task Handle(CircleCreatedEvent notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Event 2 {notification.Id}");
        return Task.CompletedTask;
    }
}