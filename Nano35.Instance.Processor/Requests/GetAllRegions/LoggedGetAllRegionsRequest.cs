using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Newtonsoft.Json;

namespace Nano35.Instance.Processor.Requests.GetAllRegions
{
    public class LoggedGetAllRegionsRequest :
        IPipelineNode<
            IGetAllRegionsRequestContract,
            IGetAllRegionsResultContract>
    {
        private readonly ILogger<LoggedGetAllRegionsRequest> _logger;
        private readonly IPipelineNode<
            IGetAllRegionsRequestContract, 
            IGetAllRegionsResultContract> _nextNode;

        public LoggedGetAllRegionsRequest(
            ILogger<LoggedGetAllRegionsRequest> logger,
            IPipelineNode<
                IGetAllRegionsRequestContract, 
                IGetAllRegionsResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetAllRegionsResultContract> Ask(
            IGetAllRegionsRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetAllRegionsLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
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