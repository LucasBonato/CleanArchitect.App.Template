using App.Domain.Todos;
using App.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.Todos;

internal sealed class TodoItemConfiguration : IEntityTypeConfiguration<TodoItem>
{
    public void Configure(EntityTypeBuilder<TodoItem> builder)
    {
        builder.HasKey(todo => todo.Id);

        builder.Property(todo => todo.DueDate).HasConversion(date => date != null ? DateTime.SpecifyKind(date.Value, DateTimeKind.Utc) : date, date => date);

        builder.HasOne<User>().WithMany().HasForeignKey(todo => todo.UserId);
    }
}
