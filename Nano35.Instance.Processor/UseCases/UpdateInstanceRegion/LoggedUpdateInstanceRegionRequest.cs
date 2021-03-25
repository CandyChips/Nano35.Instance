using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.UpdateInstanceRegion
{
    public class LoggedUpdateInstanceRegionRequest :
        PipeNodeBase<
            IUpdateInstanceRegionRequestContract,
            IUpdateInstanceRegionResultContract>
    {
        private readonly ILogger<LoggedUpdateInstanceRegionRequest> _logger;
        public LoggedUpdateInstanceRegionRequest(
            ILogger<LoggedUpdateInstanceRegionRequest> logger,
            IPipeNode<IUpdateInstanceRegionRequestContract,
                IUpdateInstanceRegionResultContract> next) : base(next)
        {
            _logger = logger;
        }

        public override async Task<IUpdateInstanceRegionResultContract> Ask(
            IUpdateInstanceRegionRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"LoggedUpdateInstanceRegion starts on: {DateTime.Now}");
            var result = await DoNext(input, cancellationToken);
            _logger.LogInformation($"LoggedUpdateInstanceRegion ends on: {DateTime.Now}");
            
            switch (result)
            {
                case IUpdateInstanceRegionSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IUpdateInstanceRegionErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}