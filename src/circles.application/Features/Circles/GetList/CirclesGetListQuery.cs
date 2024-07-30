using circles.api.contracts.Circles.Queries.GetList;
using MediatR;

namespace circles.application.Features.Circles.GetList;

public sealed record CirclesGetListQuery (CircleGetListParams Param) : IRequest<List<CircleGetListResults>>;

internal sealed record CirclesGetListQueryHandler : IRequestHandler<CirclesGetListQuery, List<CircleGetListResults>>{

    public async Task<List<CircleGetListResults>> Handle(CirclesGetListQuery request, CancellationToken cancellationToken)
    {

        var result = new List<CircleGetListResults>();

        result.Add (new CircleGetListResults(Guid.NewGuid(), "Circle1 fake"));
        result.Add (new CircleGetListResults(Guid.NewGuid(), "Circle2 fake"));

        if (request.Param.Denomination != null)
            result = result.Where (e=> e.Denomination.Contains(request.Param.Denomination)).ToList();
            
        return result;
    }
}