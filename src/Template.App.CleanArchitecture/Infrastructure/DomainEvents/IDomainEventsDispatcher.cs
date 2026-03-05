using Template.App.CleanArchitecture.Domain;

namespace Template.App.CleanArchitecture.Infrastructure.DomainEvents;

public interface IDomainEventsDispatcher
{
    Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken = default);
}
