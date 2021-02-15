using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.Requests.GetAllClientsStates
{
    public class GetAllClientStatesLogger :
        IPipelineNode<
            IGetAllClientStatesRequestContract, 
            IGetAllClientStatesResultContract>
    {
        private readonly ILogger<GetAllClientStatesLogger> _logger;
        private readonly IPipelineNode<
            IGetAllClientStatesRequestContract, 
            IGetAllClientStatesResultContract> _nextNode;

        public GetAllClientStatesLogger(
            ILogger<GetAllClientStatesLogger> logger,
            IPipelineNode<
                IGetAllClientStatesRequestContract, 
                IGetAllClientStatesResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetAllClientStatesResultContract> Ask(
            IGetAllClientStatesRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetAllClientStatesLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
            _logger.LogInformation($"GetAllClientStatesLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}