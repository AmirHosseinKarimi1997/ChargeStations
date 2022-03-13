using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenFlux.Application.Common.Behaviours;

public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : MediatR.IRequest<TResponse>
{
    private readonly ILogger _logger;

    public LoggingBehaviour(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        var requestName = typeof(TRequest).Name;

        _logger.LogInformation("----- Handling {RequestName} ({@Request})", requestName, request);
        var response = await next();
        _logger.LogInformation("----- Result {RequestName} handled - response: {@Response}", requestName, response);

        return response;
    }
}

