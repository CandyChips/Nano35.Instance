using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.GetUnitStringsByIds
{
    public class LoggedGetUnitStringsByIdsRequest :
        PipeNodeBase<
            IGetUnitStringsByIdsRequestContract,
            IGetUnitStringsByIdsResultContract>
    {
        private readonly ILogger<LoggedGetUnitStringsByIdsRequest> _logger;

        public LoggedGetUnitStringsByIdsRequest(
            ILogger<LoggedGetUnitStringsByIdsRequest> logger,
                IPipeNode<IGetUnitStringsByIdsRequestContract,
                IGetUnitStringsByIdsResultContract> next) : 
            base(next)
        {
            _logger = logger;
        }

        public override async Task<IGetUnitStringsByIdsResultContract> Ask(
            IGetUnitStringsByIdsRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetUnitStringsByIdsLogger starts on: {DateTime.Now}");
            var result = await DoNext(input, cancellationToken);
            _logger.LogInformation($"GetUnitStringsByIdsLogger ends on: {DateTime.Now}");

            switch (result)
            {
                case IGetUnitStringsByIdsSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IGetUnitStringsByIdsErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}