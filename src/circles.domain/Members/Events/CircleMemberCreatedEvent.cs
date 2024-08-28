using circles.domain.Abstractions;

namespace circles.domain.Members.Events;

public sealed record CircleMemberCreatedEvent(Guid Id) : IDomainEvent;
