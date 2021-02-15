using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAllInstanceTypes
{
    public class LoggedGetAllInstanceTypesRequest :
        IPipelineNode<IGetAllInstanceTypesRequestContract, IGetAllInstanceTypesResultContract>
    {
        private readonly ILogger<LoggedGetAllInstanceTypesRequest> _logger;
        private readonly IPipelineNode<IGetAllInstanceTypesRequestContract, IGetAllInstanceTypesResultContract> _nextNode;

        public LoggedGetAllInstanceTypesRequest(
            ILogger<LoggedGetAllInstanceTypesRequest> logger,
            IPipelineNode<IGetAllInstanceTypesRequestContract, IGetAllInstanceTypesResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetAllInstanceTypesResultContract> Ask(
            IGetAllInstanceTypesRequestContract input)
        {
            _logger.LogInformation($"GetAllInstanceTypesLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            _logger.LogInformation($"GetAllInstanceTypesLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}