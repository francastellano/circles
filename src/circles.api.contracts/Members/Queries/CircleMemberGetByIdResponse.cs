namespace circles.api.contracts.Members.Queries;

public sealed record CircleMemberGetByIdResponse(Guid Id, string CircleId, string Email, List<string> Skills);