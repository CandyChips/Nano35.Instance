using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAllCashOperations
{
    public class LoggedGetAllCashOperationsRequest :
        IPipelineNode<
            IGetAllCashOperationsRequestContract, 
            IGetAllCashOperationsResultContract>
    {
        private readonly ILogger<LoggedGetAllCashOperationsRequest> _logger;
        private readonly IPipelineNode<
            IGetAllCashOperationsRequestContract,
            IGetAllCashOperationsResultContract> _nextNode;

        public LoggedGetAllCashOperationsRequest(
            ILogger<LoggedGetAllCashOperationsRequest> logger,
            IPipelineNode<
                IGetAllCashOperationsRequestContract,
                IGetAllCashOperationsResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetAllCashOperationsResultContract> Ask(
            IGetAllCashOperationsRequestContract input)
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