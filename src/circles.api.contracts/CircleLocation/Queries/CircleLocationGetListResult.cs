namespace circles.api.contracts.ActivityLocation.Queries;

public sealed record CircleLocationGetListResult(Guid Id, string Denomination, double? Longitude, double? Latitude);
