using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Requests;

namespace Nano35.Instance.Processor.Requests.CreateClient
{
    public class CreateInstanceLogger :
        IPipelineNode<ICreateInstanceRequestContract, ICreateInstanceResultContract>
    {
        private readonly ILogger<CreateInstanceLogger> _logger;
        private readonly IPipelineNode<ICreateInstanceRequestContract, ICreateInstanceResultContract> _nextNode;

        public CreateInstanceLogger(
            ILogger<CreateInstanceLogger> logger,
            IPipelineNode<ICreateInstanceRequestContract, ICreateInstanceResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<ICreateInstanceResultContract> Ask(
            ICreateInstanceRequestContract input)
        {
            _logger.LogInformation($"CreateInstanceLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            _logger.LogInformation($"CreateInstanceLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}