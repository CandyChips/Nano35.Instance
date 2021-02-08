using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Requests;

namespace Nano35.Instance.Processor.Requests.CreateClient
{
    public class CreateClientLogger :
        IPipelineNode<ICreateClientRequestContract, ICreateClientResultContract>
    {
        private readonly ILogger<CreateClientLogger> _logger;
        private readonly IPipelineNode<ICreateClientRequestContract, ICreateClientResultContract> _nextNode;

        public CreateClientLogger(
            ILogger<CreateClientLogger> logger,
            IPipelineNode<ICreateClientRequestContract, ICreateClientResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<ICreateClientResultContract> Ask(
            ICreateClientRequestContract input)
        {
            _logger.LogInformation($"CreateClientLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            _logger.LogInformation($"CreateClientLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}