using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Todo.Core.Interfaces;
using Todo.Core.Models;

public class DatabaseLogger : ILogger
{
    private readonly ILogRepository _logRepository;
    private readonly string _categoryName;

    public DatabaseLogger(ILogRepository logRepository, string categoryName)
    {
        _logRepository = logRepository;
        _categoryName = categoryName;
    }

    public IDisposable? BeginScope<TState>(TState state) => null;

    public bool IsEnabled(LogLevel logLevel) => logLevel != LogLevel.None;

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel))
            return;

        var logEntry = new LogEntry
        {
            Level = logLevel.ToString(),
            Timestamp = DateTime.UtcNow,
            Message = formatter(state, exception), // Usa a mensagem formatada do log
            Source = _categoryName,
            Details = exception?.ToString() // Inclui detalhes da exceção, se houver
        };

        // Salva o log de forma assíncrona
        Task.Run(() => _logRepository.AddLogAsync(logEntry));
    }
}
