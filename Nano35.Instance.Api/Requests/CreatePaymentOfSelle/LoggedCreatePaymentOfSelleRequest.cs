using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.CreatePaymentOfSelle
{
    public class LoggedCreatePaymentOfSelleRequest :
        IPipelineNode<
            ICreatePaymentOfSelleRequestContract, 
            ICreatePaymentOfSelleResultContract>
    {
        private readonly ILogger<LoggedCreatePaymentOfSelleRequest> _logger;
        private readonly IPipelineNode<
            ICreatePaymentOfSelleRequestContract,
            ICreatePaymentOfSelleResultContract> _nextNode;

        public LoggedCreatePaymentOfSelleRequest(
            ILogger<LoggedCreatePaymentOfSelleRequest> logger,
            IPipelineNode<
                ICreatePaymentOfSelleRequestContract,
                ICreatePaymentOfSelleResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<ICreatePaymentOfSelleResultContract> Ask(
            ICreatePaymentOfSelleRequestContract input)
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