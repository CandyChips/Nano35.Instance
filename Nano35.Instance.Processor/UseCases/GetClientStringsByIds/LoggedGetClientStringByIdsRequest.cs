using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.GetClientStringsByIds
{
    public class LoggedGetClientStringsByIdsRequest :
        PipeNodeBase<
            IGetClientStringsByIdsRequestContract,
            IGetClientStringsByIdsResultContract>
    {
        private readonly ILogger<LoggedGetClientStringsByIdsRequest> _logger;
        

        public LoggedGetClientStringsByIdsRequest(
            ILogger<LoggedGetClientStringsByIdsRequest> logger,
            IPipeNode<IGetClientStringsByIdsRequestContract,
                IGetClientStringsByIdsResultContract> next) : 
            base(next)
        {
            _logger = logger;
        }

        public override async Task<IGetClientStringsByIdsResultContract> Ask(
            IGetClientStringsByIdsRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetClientStringsByIdsLogger starts on: {DateTime.Now}");
            var result = await DoNext(input, cancellationToken);
            _logger.LogInformation($"GetClientStringsByIdsLogger ends on: {DateTime.Now}");

            switch (result)
            {
                case IGetClientStringsByIdsSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IGetClientStringsByIdsErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}