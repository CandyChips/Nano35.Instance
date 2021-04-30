using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts;

namespace Nano35.Instance.Processor.UseCases
{
    public class LoggedPipeNode<TIn, TOut> : PipeNodeBase<TIn, TOut>
        where TIn : IRequest
        where TOut : IResponse
    {
        private readonly ILogger<TIn> _logger;
        public LoggedPipeNode(ILogger<TIn> logger, IPipeNode<TIn, TOut> next) : base(next) => _logger = logger;
        public override async Task<TOut> Ask(TIn input, CancellationToken cancellationToken)
        {
            try
            {
                var starts = DateTime.Now;
                var result = await DoNext(input, cancellationToken);
                var time = DateTime.Now - starts;
                switch (result)
                {
                    case ISuccess:
                        _logger.LogInformation($"{typeof(TIn)} ends by: {time} with success.");
                        break;
                    case IError error:
                        _logger.LogInformation($"{typeof(TIn)} ends by: {time} with error: {error}.");
                        break;
                    default:
                        _logger.LogInformation($"{typeof(TIn)} ends by: {time} with strange error!!!");
                        break;
                }
                return result;
            }
            catch (Exception e)
            {
                _logger.LogInformation($"{typeof(TIn)} ends by: {DateTime.Now} with exception!!!");
                throw;
            }
        }
    }
}