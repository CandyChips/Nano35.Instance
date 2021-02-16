using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.CreateCashOutput
{
    public class LoggedCreateCashOutputRequest :
        IPipelineNode<
            ICreateCashOutputRequestContract,
            ICreateCashOutputResultContract>
    {
        private readonly ILogger<LoggedCreateCashOutputRequest> _logger;
        private readonly IPipelineNode<
            ICreateCashOutputRequestContract,
            ICreateCashOutputResultContract> _nextNode;

        public LoggedCreateCashOutputRequest(
            ILogger<LoggedCreateCashOutputRequest> logger,
            IPipelineNode<
                ICreateCashOutputRequestContract,
                ICreateCashOutputResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<ICreateCashOutputResultContract> Ask(
            ICreateCashOutputRequestContract input)
        {
            _logger.LogInformation($"Create cash output logger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            _logger.LogInformation($"Create cash output logger ends on: {DateTime.Now}");
            
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