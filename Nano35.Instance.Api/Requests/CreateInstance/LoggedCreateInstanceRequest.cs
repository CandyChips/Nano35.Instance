using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.CreateInstance
{
    public class LoggedCreateInstanceRequest :
        IPipelineNode<
            ICreateInstanceRequestContract, 
            ICreateInstanceResultContract>
    {
        private readonly ILogger<LoggedCreateInstanceRequest> _logger;
        private readonly IPipelineNode<
            ICreateInstanceRequestContract,
            ICreateInstanceResultContract> _nextNode;

        public LoggedCreateInstanceRequest(
            ILogger<LoggedCreateInstanceRequest> logger,
            IPipelineNode<
                ICreateInstanceRequestContract,
                ICreateInstanceResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<ICreateInstanceResultContract> Ask(
            ICreateInstanceRequestContract input)
        {
            _logger.LogInformation($"CreateClientLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            _logger.LogInformation($"CreateClientLogger ends on: {DateTime.Now}");
            
            switch (result)
            {
                case IGetAllRegionsSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IGetAllRegionsErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}