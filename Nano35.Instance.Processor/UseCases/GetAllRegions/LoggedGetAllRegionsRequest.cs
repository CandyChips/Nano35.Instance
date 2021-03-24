using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.GetAllRegions
{
    public class LoggedGetAllRegionsRequest :
        PipeNodeBase<
            IGetAllRegionsRequestContract,
            IGetAllRegionsResultContract>
    {
        private readonly ILogger<LoggedGetAllRegionsRequest> _logger;

        public LoggedGetAllRegionsRequest(ILogger<LoggedGetAllRegionsRequest> logger,
            IPipeNode<IGetAllRegionsRequestContract, IGetAllRegionsResultContract> next) : base(next)
        {
            _logger = logger;
        }

        public override async Task<IGetAllRegionsResultContract> Ask(
            IGetAllRegionsRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetAllRegionsLogger starts on: {DateTime.Now}");
            var result = await DoNext(input, cancellationToken);
            _logger.LogInformation($"GetAllRegionsLogger ends on: {DateTime.Now}");

            switch (result)
            {
                case IGetAllRegionsSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IGetAllRegionsErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}