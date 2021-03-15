using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetUnitStringById
{
    public class LoggedGetUnitStringByIdRequest :
        IPipelineNode<IGetUnitStringByIdRequestContract, IGetUnitStringByIdResultContract>
    {
        private readonly ILogger<LoggedGetUnitStringByIdRequest> _logger;
        private readonly IPipelineNode<IGetUnitStringByIdRequestContract, IGetUnitStringByIdResultContract> _nextNode;

        public LoggedGetUnitStringByIdRequest(
            ILogger<LoggedGetUnitStringByIdRequest> logger,
            IPipelineNode<IGetUnitStringByIdRequestContract, IGetUnitStringByIdResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetUnitStringByIdResultContract> Ask(
            IGetUnitStringByIdRequestContract input)
        {
            _logger.LogInformation($"GetUnitStringByIdLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            switch (result)
            {
                case IGetUnitStringByIdSuccessResultContract:
                    _logger.LogInformation($"GetUnitStringByIdLogger ends on: {DateTime.Now} with success");
                    break;
                case IGetUnitStringByIdErrorResultContract error:
                    _logger.LogError($"GetUnitStringByIdLogger ends on: {DateTime.Now} with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}