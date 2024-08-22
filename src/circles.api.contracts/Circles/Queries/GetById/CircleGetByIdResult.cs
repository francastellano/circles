namespace circles.api.contracts.Circles.Queries.GetById;

public sealed record CircleGetByIdResult(Guid Id, string Denomination, string Creator);