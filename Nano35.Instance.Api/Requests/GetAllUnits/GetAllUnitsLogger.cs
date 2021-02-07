using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAllUnits
{
    public class GetAllUnitsLogger :
        IPipelineNode<IGetAllUnitsRequestContract, IGetAllUnitsResultContract>
    {
        private readonly ILogger<GetAllUnitsLogger> _logger;
        private readonly IPipelineNode<IGetAllUnitsRequestContract, IGetAllUnitsResultContract> _nextNode;

        public GetAllUnitsLogger(
            ILogger<GetAllUnitsLogger> logger,
            IPipelineNode<IGetAllUnitsRequestContract, IGetAllUnitsResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetAllUnitsResultContract> Ask(
            IGetAllUnitsRequestContract input)
        {
            _logger.LogInformation($"GetAllUnitsLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            _logger.LogInformation($"GetAllUnitsLogger ends on: {DateTime.Now}");
            _logger.LogInformation("");
            return result;
        }
    }
}