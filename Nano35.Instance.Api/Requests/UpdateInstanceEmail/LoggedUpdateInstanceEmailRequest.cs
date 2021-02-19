using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.UpdateInstanceEmail
{
    public class LoggedUpdateInstanceEmailRequest :
        IPipelineNode<IUpdateInstanceEmailRequestContract, IUpdateInstanceEmailResultContract>
    {
        private readonly ILogger<LoggedUpdateInstanceEmailRequest> _logger;
        private readonly IPipelineNode<IUpdateInstanceEmailRequestContract, IUpdateInstanceEmailResultContract> _nextNode;

        public LoggedUpdateInstanceEmailRequest(
            ILogger<LoggedUpdateInstanceEmailRequest> logger,
            IPipelineNode<IUpdateInstanceEmailRequestContract, IUpdateInstanceEmailResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IUpdateInstanceEmailResultContract> Ask(
            IUpdateInstanceEmailRequestContract input)
        {
            _logger.LogInformation($"UpdateInstanceEmailLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            _logger.LogInformation($"UpdateInstanceEmailLogger ends on: {DateTime.Now}");
            
            switch (result)
            {
                case IUpdateInstanceEmailSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IUpdateInstanceEmailErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}