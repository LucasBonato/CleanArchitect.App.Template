namespace App.Domain;

public record Error(
    string Code,
    string Description,
    ErrorType Type
) {
    public static readonly Error None = new(string.Empty, string.Empty, ErrorType.FAILURE);
    public static readonly Error NullValue = new("General.Null", "Null value was provided", ErrorType.FAILURE);

    public static Error Failure(string code, string description) =>
        new(code, description, ErrorType.FAILURE);

    public static Error NotFound(string code, string description) =>
        new(code, description, ErrorType.NOT_FOUND);

    public static Error Problem(string code, string description) =>
        new(code, description, ErrorType.PROBLEM);

    public static Error Conflict(string code, string description) =>
        new(code, description, ErrorType.CONFLICT);
}
