using circles.api.contracts.Abstractions.Paginations;

namespace circles.api.contracts.Members.Queries;

public sealed record CircleGetListMembersRequest(Guid CircleId) : PaginationParams;