namespace circles.api.contracts.Activities.Queries;

public sealed record ActivityGetListRequest(Guid CircleId, string? Denomination);