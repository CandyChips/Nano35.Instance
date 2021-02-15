using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.CreateClient
{
    public class LoggedCreateClientRequest :
        IPipelineNode<
            ICreateClientRequestContract, 
            ICreateClientResultContract>
    {
        private readonly ILogger<LoggedCreateClientRequest> _logger;
        
        private readonly IPipelineNode<
            ICreateClientRequestContract, 
            ICreateClientResultContract> _nextNode;

        public LoggedCreateClientRequest(
            ILogger<LoggedCreateClientRequest> logger,
            IPipelineNode<
                ICreateClientRequestContract, 
                ICreateClientResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<ICreateClientResultContract> Ask(
            ICreateClientRequestContract input)
        {
            _logger.LogInformation($"Create client logger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            _logger.LogInformation($"Create client logger ends on: {DateTime.Now}");
            return result;
        }
    }
}