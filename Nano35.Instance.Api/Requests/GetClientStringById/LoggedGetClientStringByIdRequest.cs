using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetClientStringById
{
    public class LoggedGetClientStringByIdRequest :
        IPipelineNode<IGetClientStringByIdRequestContract, IGetClientStringByIdResultContract>
    {
        private readonly ILogger<LoggedGetClientStringByIdRequest> _logger;
        private readonly IPipelineNode<IGetClientStringByIdRequestContract, IGetClientStringByIdResultContract> _nextNode;

        public LoggedGetClientStringByIdRequest(
            ILogger<LoggedGetClientStringByIdRequest> logger,
            IPipelineNode<IGetClientStringByIdRequestContract, IGetClientStringByIdResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetClientStringByIdResultContract> Ask(
            IGetClientStringByIdRequestContract input)
        {
            _logger.LogInformation($"GetClientStringByIdLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            switch (result)
            {
                case IGetClientStringByIdSuccessResultContract:
                    _logger.LogInformation($"GetClientStringByIdLogger ends on: {DateTime.Now} with success");
                    break;
                case IGetClientStringByIdErrorResultContract error:
                    _logger.LogError($"GetClientStringByIdLogger ends on: {DateTime.Now} with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}