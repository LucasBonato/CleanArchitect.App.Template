using Anv;

namespace App;

public static class AppEnv {
    public static readonly AnvEnv OTEL_SERVICE_NAME = new("OTEL_SERVICE_NAME");
}
