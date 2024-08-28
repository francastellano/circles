using circles.domain.Abstractions;
using MediatR;

namespace circles.application.Abstractions.Messages;
public interface IQuery<TResponse> : IRequest<Result<TResponse>>, IBaseQuery;
public interface IBaseQuery;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>;