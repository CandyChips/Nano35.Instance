using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests
{
    public class LoggedPipeNode<TIn, TOut> : PipeNodeBase<TIn, TOut>
        where TIn : IRequest
        where TOut : IResponse
    {
        private readonly ILogger<TIn> _logger;
        public LoggedPipeNode(ILogger<TIn> logger, IPipeNode<TIn, TOut> next) : base(next) => _logger = logger;
        public override async Task<TOut> Ask(TIn input)
        {
            var starts = DateTime.Now;
            var result = await DoNext(input);
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
    }
    public class LoggedUseCasePipeNode<TIn, TOut> : UseCasePipeNodeBase<TIn, TOut>
        where TIn : IRequest
        where TOut : class
    {
        private readonly ILogger<TIn> _logger;
        public LoggedUseCasePipeNode(ILogger<TIn> logger, IUseCasePipeNode<TIn, TOut> next) : base(next) => _logger = logger;
        public override async Task<UseCaseResponse<TOut>> Ask(TIn input)
        {
            try
            {
                var starts = DateTime.Now;
                var result = await DoNext(input);
                var time = DateTime.Now - starts;
                _logger.LogInformation(result.IsSuccess()
                    ? $"{typeof(TIn)} ends by: {time} with success."
                    : $"{typeof(TIn)} ends by: {time} with error: {result.Error}.");
                return result;
            }
            catch (Exception e)
            {
                _logger.LogInformation($"{typeof(TIn)} ends by: {DateTime.Now} with exception!!!");
                return new UseCaseResponse<TOut>($"{typeof(TIn)} ends by: {DateTime.Now} with exception!!!");
            }
        }
    }
}