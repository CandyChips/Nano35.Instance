using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetInstanceById
{
    public class LoggedGetInstanceByIdRequest :
        IPipelineNode<IGetInstanceByIdRequestContract, IGetInstanceByIdResultContract>
    {
        private readonly ILogger<LoggedGetInstanceByIdRequest> _logger;
        private readonly IPipelineNode<IGetInstanceByIdRequestContract, IGetInstanceByIdResultContract> _nextNode;

        public LoggedGetInstanceByIdRequest(
            ILogger<LoggedGetInstanceByIdRequest> logger,
            IPipelineNode<IGetInstanceByIdRequestContract, IGetInstanceByIdResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetInstanceByIdResultContract> Ask(
            IGetInstanceByIdRequestContract input)
        {
            _logger.LogInformation($"GetInstanceByIdLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            _logger.LogInformation($"GetInstanceByIdLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}