using Template.App.CleanArchitecture.Domain;

namespace Template.App.CleanArchitecture.Infrastructure.Time;

internal sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
