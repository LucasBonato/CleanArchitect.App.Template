using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Template.App.CleanArchitecture.Domain.Todos;
using Template.App.CleanArchitecture.Domain.Users;

namespace Template.App.CleanArchitecture.Infrastructure.Todos;

internal sealed class TodoItemConfiguration : IEntityTypeConfiguration<TodoItem>
{
    public void Configure(EntityTypeBuilder<TodoItem> builder)
    {
        builder.HasKey(todo => todo.Id);

        builder.Property(todo => todo.DueDate)
            .HasConversion(date => date != null ? DateTime.SpecifyKind(date.Value, DateTimeKind.Utc) : date, date => date);

        builder.Property(todo => todo.Description)
            .HasMaxLength(255);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(todo => todo.UserId);
    }
}
