using Microsoft.EntityFrameworkCore;
using Template.App.CleanArchitecture.Domain.Todos;
using Template.App.CleanArchitecture.Domain.Users;

namespace Template.App.CleanArchitecture.Application.Abstractions.Data;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    DbSet<TodoItem> TodoItems { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
