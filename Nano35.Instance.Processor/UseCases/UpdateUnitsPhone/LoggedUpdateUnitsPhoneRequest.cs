using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.UpdateUnitsPhone
{
    public class LoggedUpdateUnitsPhoneRequest :
        IPipelineNode<IUpdateUnitsPhoneRequestContract, IUpdateUnitsPhoneResultContract>
    {
        private readonly ILogger<LoggedUpdateUnitsPhoneRequest> _logger;
        
        private readonly IPipelineNode<
            IUpdateUnitsPhoneRequestContract, 
            IUpdateUnitsPhoneResultContract> _nextNode;

        public LoggedUpdateUnitsPhoneRequest(
            ILogger<LoggedUpdateUnitsPhoneRequest> logger,
            IPipelineNode<
                IUpdateUnitsPhoneRequestContract, 
                IUpdateUnitsPhoneResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IUpdateUnitsPhoneResultContract> Ask(
            IUpdateUnitsPhoneRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"LoggedUpdateUnitsPhone starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
            _logger.LogInformation($"LoggedUpdateUnitsPhone ends on: {DateTime.Now}");
            
            switch (result)
            {
                case IUpdateUnitsPhoneSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IUpdateUnitsPhoneErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}