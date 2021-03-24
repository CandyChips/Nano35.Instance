using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.GetClientStringById
{
    public class LoggedGetClientStringByIdRequest :
        PipeNodeBase<
            IGetClientStringByIdRequestContract,
            IGetClientStringByIdResultContract>
    {
        private readonly ILogger<LoggedGetClientStringByIdRequest> _logger;
        
        public LoggedGetClientStringByIdRequest(
            ILogger<LoggedGetClientStringByIdRequest> logger,
            IPipeNode<IGetClientStringByIdRequestContract,
                IGetClientStringByIdResultContract> next) : base(next)
        {
            _logger = logger;
        }

        public override async Task<IGetClientStringByIdResultContract> Ask(
            IGetClientStringByIdRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetClientStringByIdLogger starts on: {DateTime.Now}");
            var result = await DoNext(input, cancellationToken);
            _logger.LogInformation($"GetClientStringByIdLogger ends on: {DateTime.Now}");

            switch (result)
            {
                case IGetClientStringByIdSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IGetClientStringByIdErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}