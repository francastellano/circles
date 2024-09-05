namespace circles.api.contracts.ActivityMembers.Commands;

public sealed record ActivityMemberAddRequest(Guid ActivityId, Guid MemberId);