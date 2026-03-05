using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Template.App.CleanArchitecture.Domain.Users;

namespace Template.App.CleanArchitecture.Infrastructure.Users;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(user => user.Id);

        builder.HasIndex(user => user.Email).IsUnique();

        builder.Property(user => user.Email).HasMaxLength(200).IsRequired();

        builder.Property(user => user.FirstName).HasMaxLength(50);
        builder.Property(user => user.LastName).HasMaxLength(50);
    }
}
