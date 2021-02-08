using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Requests;

namespace Nano35.Instance.Processor.Requests.CreateUnit
{
    public class CreateUnitLogger :
        IPipelineNode<ICreateUnitRequestContract, ICreateUnitResultContract>
    {
        private readonly ILogger<CreateUnitLogger> _logger;
        private readonly IPipelineNode<ICreateUnitRequestContract, ICreateUnitResultContract> _nextNode;

        public CreateUnitLogger(
            ILogger<CreateUnitLogger> logger,
            IPipelineNode<ICreateUnitRequestContract, ICreateUnitResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<ICreateUnitResultContract> Ask(
            ICreateUnitRequestContract input)
        {
            _logger.LogInformation($"CreateUnitLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            _logger.LogInformation($"CreateUnitLogger ends on: {DateTime.Now}");
            _logger.LogInformation("");
            return result;
        }
    }
}