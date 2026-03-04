using App.Domain.Todos;

using App.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace App.Application.Abstractions.Data;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    DbSet<TodoItem> TodoItems { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
