namespace circles.api.contracts.Goals.Commands;

public sealed record CircleGoalAddRequest(Guid CircleId, string Denomination);