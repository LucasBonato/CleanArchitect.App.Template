namespace Template.App.CleanArchitecture.Domain;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}
