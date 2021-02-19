using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.CreatePaymentOfComing
{
    public class LoggedCreatePaymentOfComingRequest :
        IPipelineNode<
            ICreatePaymentOfComingRequestContract, 
            ICreatePaymentOfComingResultContract>
    {
        private readonly ILogger<LoggedCreatePaymentOfComingRequest> _logger;
        private readonly IPipelineNode<
            ICreatePaymentOfComingRequestContract,
            ICreatePaymentOfComingResultContract> _nextNode;

        public LoggedCreatePaymentOfComingRequest(
            ILogger<LoggedCreatePaymentOfComingRequest> logger,
            IPipelineNode<
                ICreatePaymentOfComingRequestContract,
                ICreatePaymentOfComingResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<ICreatePaymentOfComingResultContract> Ask(
            ICreatePaymentOfComingRequestContract input)
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