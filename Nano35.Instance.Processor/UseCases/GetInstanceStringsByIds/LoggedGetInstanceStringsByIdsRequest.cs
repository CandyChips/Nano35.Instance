using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.GetInstanceStringsByIds
{
    public class LoggedGetInstanceStringsByIdsRequest :
        PipeNodeBase<
            IGetInstanceStringsByIdsRequestContract,
            IGetInstanceStringsByIdsResultContract>
    {
        private readonly ILogger<LoggedGetInstanceStringsByIdsRequest> _logger;
        

        public LoggedGetInstanceStringsByIdsRequest(
            ILogger<LoggedGetInstanceStringsByIdsRequest> logger,
            IPipeNode<IGetInstanceStringsByIdsRequestContract,
                IGetInstanceStringsByIdsResultContract> next) : 
            base(next)
        {
            _logger = logger;
        }

        public override async Task<IGetInstanceStringsByIdsResultContract> Ask(
            IGetInstanceStringsByIdsRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetInstanceStringsByIdsLogger starts on: {DateTime.Now}");
            var result = await DoNext(input, cancellationToken);
            _logger.LogInformation($"GetInstanceStringsByIdsLogger ends on: {DateTime.Now}");

            switch (result)
            {
                case IGetInstanceStringsByIdsSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IGetInstanceStringsByIdsErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}