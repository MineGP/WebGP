using MediatR.Pipeline;
using Serilog;

namespace WebGP.Application.Common.Behaviours;

public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest>
    where TRequest : notnull
{
    private readonly ILogger _logger;

    public LoggingBehaviour(ILogger logger)
    {
        _logger = logger;
    }

    public Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        // auth user id/name logging should be there

        _logger.Information($"New request: {requestName} : {request}");
        return Task.CompletedTask;
    }
}