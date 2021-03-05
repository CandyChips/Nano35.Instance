using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAllUnits
{
    public class LoggedGetAllUnitsRequest :
        IPipelineNode<
            IGetAllUnitsRequestContract, 
            IGetAllUnitsResultContract>
    {
        private readonly ILogger<LoggedGetAllUnitsRequest> _logger;
        
        private readonly IPipelineNode<
            IGetAllUnitsRequestContract,
            IGetAllUnitsResultContract> _nextNode;

        public LoggedGetAllUnitsRequest(
            ILogger<LoggedGetAllUnitsRequest> logger,
            IPipelineNode<
                IGetAllUnitsRequestContract, 
                IGetAllUnitsResultContract> nextNode)
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
            
            switch (result)
            {
                case IGetAllUnitsSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IGetAllUnitsErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }

            return result;
        }
    }
}