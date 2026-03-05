using Template.App.CleanArchitecture.Application.Abstractions.Messaging;
using Template.App.CleanArchitecture.Domain;

namespace Template.App.CleanArchitecture.Application.Abstractions.Behaviors;

internal static class LoggingDecorator
{
    internal sealed class CommandHandler<TCommand, TResponse>(
        ICommandHandler<TCommand, TResponse> innerHandler,
        ILogger<CommandHandler<TCommand, TResponse>> logger
    ) : ICommandHandler<TCommand, TResponse> where TCommand : ICommand<TResponse> {
        public async Task<Result<TResponse>> Handle(TCommand command, CancellationToken cancellationToken)
        {
            if (logger.IsEnabled(LogLevel.Information))
                logger.LogInformation("Processing command {Command}", typeof(TCommand).Name);

            Result<TResponse> result = await innerHandler.Handle(command, cancellationToken);

            if (result.IsSuccess)
            {
                if (logger.IsEnabled(LogLevel.Information))
                    logger.LogInformation("Completed command {Command}", typeof(TCommand).Name);
                return result;
            }

            using (logger.BeginScope(new Dictionary<string, object> { ["Error"] = result.Error }))
                if (logger.IsEnabled(LogLevel.Error))
                    logger.LogError("Completed command {Command} with error", typeof(TCommand).Name);

            return result;
        }
    }

    internal sealed class CommandBaseHandler<TCommand>(
        ICommandHandler<TCommand> innerHandler,
        ILogger<CommandBaseHandler<TCommand>> logger
    ) : ICommandHandler<TCommand> where TCommand : ICommand {
        public async Task<Result> Handle(TCommand command, CancellationToken cancellationToken)
        {
            if (logger.IsEnabled(LogLevel.Information))
                logger.LogInformation("Processing command {Command}", typeof(TCommand).Name);

            Result result = await innerHandler.Handle(command, cancellationToken);

            if (result.IsSuccess)
            {
                if (logger.IsEnabled(LogLevel.Information))
                    logger.LogInformation("Completed command {Command}", typeof(TCommand).Name);
                return result;
            }

            using (logger.BeginScope(new Dictionary<string, object> { ["Error"] = result.Error }))
                if (logger.IsEnabled(LogLevel.Error))
                    logger.LogError("Completed command {Command} with error", typeof(TCommand).Name);

            return result;
        }
    }

    internal sealed class QueryHandler<TQuery, TResponse>(
        IQueryHandler<TQuery, TResponse> innerHandler,
        ILogger<QueryHandler<TQuery, TResponse>> logger
    ) : IQueryHandler<TQuery, TResponse> where TQuery : IQuery<TResponse> {
        public async Task<Result<TResponse>> Handle(TQuery query, CancellationToken cancellationToken)
        {
            if (logger.IsEnabled(LogLevel.Information))
                logger.LogInformation("Processing query {Query}", typeof(TQuery).Name);

            Result<TResponse> result = await innerHandler.Handle(query, cancellationToken);

            if (result.IsSuccess)
            {
                if (logger.IsEnabled(LogLevel.Information))
                    logger.LogInformation("Completed query {Query}", typeof(TQuery).Name);
                return result;
            }

            using (logger.BeginScope(new Dictionary<string, object> { ["Error"] = result.Error }))
                if (logger.IsEnabled(LogLevel.Error))
                    logger.LogError("Completed query {Query} with error", typeof(TQuery).Name);

            return result;
        }
    }
}
