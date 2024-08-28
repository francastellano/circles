using circles.domain.Abstractions;

namespace circles.domain.MemberSkills.Events;

public sealed record MemberSkillCreatedEvent(Guid Id) : IDomainEvent;