namespace circles.api.contracts.Members.Queries;

public sealed record CircleListGetMemberResult(Guid Id, Guid CircleId, string Email, string Name);
