using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Nano35.Instance.Api.Services.Requests.Behaviours
{
    public class LoggerCommandDecorator<TIn, TOut> :
        IPipelineBehavior<TIn, TOut>
        where TIn : ICommandRequest<TOut>
    {
        private readonly ILogger<LoggerCommandDecorator<TIn, TOut>> _logger;

        public LoggerCommandDecorator(
            ILogger<LoggerCommandDecorator<TIn, TOut>> logger)
        {
            _logger = logger;
        }

        public async Task<TOut> Handle(TIn request, CancellationToken cancellationToken, RequestHandlerDelegate<TOut> next)
        {
            _logger.LogInformation("Before Command");
            var response = await next();
            _logger.LogInformation("After Command");
            return response;
        }
    }

    public class LoggerQueryDecorator<TIn, TOut> :
        IPipelineBehavior<TIn, TOut>
        where TIn : IQueryRequest<TOut>
    {
        private readonly ILogger<LoggerQueryDecorator<TIn, TOut>> _logger;

        public LoggerQueryDecorator(
            ILogger<LoggerQueryDecorator<TIn, TOut>> logger)
        {
            _logger = logger;
        }

        public async Task<TOut> Handle(
            TIn request,
            CancellationToken cancellationToken,
            RequestHandlerDelegate<TOut> next)
        {
            _logger.LogInformation("Before Query");
            var response = await next();
            _logger.LogInformation("After Query");
            return response;
        }
    }
}