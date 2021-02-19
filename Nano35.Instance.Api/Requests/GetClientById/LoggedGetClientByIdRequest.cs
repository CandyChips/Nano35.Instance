using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetClientById
{
    public class LoggedGetClientByIdRequest :
        IPipelineNode<IGetClientByIdRequestContract, IGetClientByIdResultContract>
    {
        private readonly ILogger<LoggedGetClientByIdRequest> _logger;
        private readonly IPipelineNode<IGetClientByIdRequestContract, IGetClientByIdResultContract> _nextNode;

        public LoggedGetClientByIdRequest(
            ILogger<LoggedGetClientByIdRequest> logger,
            IPipelineNode<IGetClientByIdRequestContract, IGetClientByIdResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetClientByIdResultContract> Ask(
            IGetClientByIdRequestContract input)
        {
            _logger.LogInformation($"GetClientByIdLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            _logger.LogInformation($"GetClientByIdLogger ends on: {DateTime.Now}");
            
            switch (result)
            {
                case IGetClientByIdSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IGetClientByIdErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}