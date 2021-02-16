using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.Requests.GetAllCashOperations
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
            IGetAllCashOperationsRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetAllCashOperationsLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
            _logger.LogInformation($"GetAllCashOperationsLogger ends on: {DateTime.Now}");
            
            switch (result)
            {
                case IGetAllCashOperationsSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IGetAllCashOperationsErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}