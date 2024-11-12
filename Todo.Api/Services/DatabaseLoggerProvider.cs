using Microsoft.Extensions.Logging;
using Todo.Core.Interfaces;

public class DatabaseLoggerProvider : ILoggerProvider
{
    private readonly ILogRepository _logRepository;

    public DatabaseLoggerProvider(ILogRepository logRepository)
    {
        _logRepository = logRepository;
    }

    public ILogger CreateLogger(string categoryName)
    {
        return new DatabaseLogger(_logRepository, categoryName);
    }

    public void Dispose() { }
}
