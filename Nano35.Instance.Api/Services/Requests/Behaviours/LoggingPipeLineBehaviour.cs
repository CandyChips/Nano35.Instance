using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Nano35.Instance.Api.Services.Requests.Behaviours
{
    public class LoggingPipeLineBehaviour<TRequest, TResponse> 
                : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<LoggingPipeLineBehaviour<TRequest, TResponse>> _logger;
        public LoggingPipeLineBehaviour(
            ILogger<LoggingPipeLineBehaviour<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(
            TRequest request, 
            CancellationToken cancellationToken, 
            RequestHandlerDelegate<TResponse> next)
        {
            var response = await next().ConfigureAwait(false);
            return response;
        }
    }
}