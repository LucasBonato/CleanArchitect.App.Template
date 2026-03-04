namespace App.Domain;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}
