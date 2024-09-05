namespace circles.api.contracts.Activities.Queries;

public sealed record ActivityGetByIdResults(Guid ActivityId, string Denomination, DateTime DateInit, string LocationDenomination, double? LocationLatitude, double? LocationLongitude, List<string> Members);