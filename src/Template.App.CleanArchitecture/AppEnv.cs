using Anv;

namespace Template.App.CleanArchitecture;

public static class AppEnv {
    public static readonly AnvEnv OTEL_SERVICE_NAME = new("OTEL_SERVICE_NAME");
}
