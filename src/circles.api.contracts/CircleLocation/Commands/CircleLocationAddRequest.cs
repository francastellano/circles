namespace circles.api.contracts.CircleLocation.Commands;

public sealed record CircleLocationAddRequest (Guid CircleId, string Denomination, double? latitude, double? longitude);