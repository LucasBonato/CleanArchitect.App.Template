using Template.App.CleanArchitecture.Domain;

namespace Template.App.CleanArchitecture.Application.Abstractions.Messaging;

public interface IQueryHandler<in TQuery, TResponse> where TQuery : IQuery<TResponse>
{
    Task<Result<TResponse>> Handle(TQuery query, CancellationToken cancellationToken);
}
