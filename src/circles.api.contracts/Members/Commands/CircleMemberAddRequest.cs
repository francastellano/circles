namespace circles.api.contracts.Members.Commands;

public sealed record CircleMemberAddRequest(Guid CircleId, string Email, string Name);
