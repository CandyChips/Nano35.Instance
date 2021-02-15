using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAllClientStates
{
    public class LoggedGetAllClientStates :
        IPipelineNode<IGetAllClientStatesRequestContract, IGetAllClientStatesResultContract>
    {
        private readonly ILogger<LoggedGetAllClientStates> _logger;
        private readonly IPipelineNode<IGetAllClientStatesRequestContract, IGetAllClientStatesResultContract> _nextNode;

        public LoggedGetAllClientStates(
            ILogger<LoggedGetAllClientStates> logger,
            IPipelineNode<IGetAllClientStatesRequestContract, IGetAllClientStatesResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetAllClientStatesResultContract> Ask(
            IGetAllClientStatesRequestContract input)
        {
            _logger.LogInformation($"GetAllClientStatesLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            _logger.LogInformation($"GetAllClientStatesLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}