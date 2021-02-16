using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.Requests.CreateInstance
{
    public class LoggedCreateInstanceRequest :
        IPipelineNode<ICreateInstanceRequestContract, ICreateInstanceResultContract>
    {
        private readonly ILogger<LoggedCreateInstanceRequest> _logger;
        private readonly IPipelineNode<ICreateInstanceRequestContract, ICreateInstanceResultContract> _nextNode;

        public LoggedCreateInstanceRequest(
            ILogger<LoggedCreateInstanceRequest> logger,
            IPipelineNode<ICreateInstanceRequestContract, ICreateInstanceResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<ICreateInstanceResultContract> Ask(ICreateInstanceRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CreateInstanceLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
            _logger.LogInformation($"CreateInstanceLogger ends on: {DateTime.Now}");
            
            switch (result)
            {
                case ICreateInstanceSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case ICreateInstanceErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}