namespace circles.api.contracts.Activities.Commands;

public sealed record CircleActivityAddRequest(Guid CircleId, Guid LocationId, string Denomination);