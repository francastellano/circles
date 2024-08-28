using circles.domain.Abstractions;

namespace circles.domain.Activities.Events;
public sealed record ActivityIsCreatedEvent(Guid Id) : IDomainEvent;