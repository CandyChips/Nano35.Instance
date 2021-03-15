using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetInstanceStringById
{
    public class LoggedGetInstanceStringByIdRequest :
        IPipelineNode<IGetInstanceStringByIdRequestContract, IGetInstanceStringByIdResultContract>
    {
        private readonly ILogger<LoggedGetInstanceStringByIdRequest> _logger;
        private readonly IPipelineNode<IGetInstanceStringByIdRequestContract, IGetInstanceStringByIdResultContract> _nextNode;

        public LoggedGetInstanceStringByIdRequest(
            ILogger<LoggedGetInstanceStringByIdRequest> logger,
            IPipelineNode<IGetInstanceStringByIdRequestContract, IGetInstanceStringByIdResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetInstanceStringByIdResultContract> Ask(
            IGetInstanceStringByIdRequestContract input)
        {
            _logger.LogInformation($"GetInstanceStringByIdLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            switch (result)
            {
                case IGetInstanceStringByIdSuccessResultContract:
                    _logger.LogInformation($"GetInstanceStringByIdLogger ends on: {DateTime.Now} with success");
                    break;
                case IGetInstanceStringByIdErrorResultContract error:
                    _logger.LogError($"GetInstanceStringByIdLogger ends on: {DateTime.Now} with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}