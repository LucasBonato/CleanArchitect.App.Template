export PROJECT := "Template.App.CleanArchitecture"

set dotenv-filename := "src/{{PROJECT}}/.env"

# List all recipes
default:
    @just --list

# ---------------------------
# Development
# ---------------------------

# Run the project
[group("dev")]
run:
    dotnet run --project src/{{PROJECT}}

# Run the project with watch
[group("dev")]
watch:
    dotnet watch run --project src/{{PROJECT}}

# Restore the project
[group("dev")]
restore:
    dotnet restore

# Build the project
[group("dev")]
build:
    dotnet build

# Clean Solution
[group("dev")]
clean:
    dotnet clean

# Create new Migration using Entity Framework
[group("dev")]
add-migration migration:
    dotnet ef migrations add {{migration}} \
      -o Infrastructure/Database/Migrations/ \
      -p src/{{PROJECT}}/{{PROJECT}}.csproj \
      -s src/{{PROJECT}}/{{PROJECT}}.csproj

# ---------------------------
# Testing
# ---------------------------

# Run all tests
[group("test")]
test:
    dotnet test tests/*

# Run all tests with watch
[group("test")]
test-watch:
    dotnet watch test tests/*

# Run all tests with coverage
[group("test")]
coverage:
    dotnet test tests/* --collect:"XPlat Code Coverage"

# ---------------------------
# Formatting & Quality
# ---------------------------

# Format code
[group("format")]
format:
    dotnet format

# Show warnings of the lint
[group("format")]
lint:
    dotnet build -warnaserror

# Run all checks for format and tests
[group("format")]
check: format lint test
    @echo "All checks passed"

# ---------------------------
# Containers
# ---------------------------

# Up all containers from compose
[group("containers")]
up:
    docker compose up -d

# Dowm all containers from compose
[group("containers")]
down:
    docker compose down

# See logs of containers
[group("containers")]
logs:
    docker compose logs -f

# Rebuild containers
[group("containers")]
rebuild:
    docker compose up -d --build

# Process Status of containers
[group("containers")]
ps:
    docker compose ps

# ---------------------------
# Database
# ---------------------------

# Connect to the database container
[group("database")]
db:
    docker compose exec postgres psql -U postgres -d clean-architecture

# Reset containers
[group("database")]
db-reset:
    docker compose down -v
    docker compose up -d

# ---------------------------
# OpenTelemetry / Aspire
# ---------------------------

# Show url for dashboard OpenTelemetry is exporting
[group("otel")]
dashboard:
    @echo "Aspire dashboard: http://localhost:18888"

# ---------------------------
# Utilities
# ---------------------------

# List all packages of the solution
[group("util")]
deps:
    dotnet list package

# Show outdated packages
[group("util")]
outdated:
    dotnet list package --outdated