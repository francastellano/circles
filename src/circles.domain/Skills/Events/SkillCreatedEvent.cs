using circles.domain.Abstractions;

namespace circles.domain.Skills.Events;

public sealed record SkillCreatedEvent(Guid Id) : IDomainEvent;