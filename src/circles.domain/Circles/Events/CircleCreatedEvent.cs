using circles.domain.Abstractions;

namespace circles.domain.Circles.Events;

public sealed record CircleCreatedEvent(Guid Id) : IDomainEvent;