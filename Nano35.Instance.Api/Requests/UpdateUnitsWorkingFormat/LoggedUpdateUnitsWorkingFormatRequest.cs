using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.UpdateUnitsWorkingFormat
{
    public class LoggedUpdateUnitsWorkingFormatRequest :
        IPipelineNode<IUpdateUnitsWorkingFormatRequestContract, IUpdateUnitsWorkingFormatResultContract>
    {
        private readonly ILogger<LoggedUpdateUnitsWorkingFormatRequest> _logger;
        private readonly IPipelineNode<IUpdateUnitsWorkingFormatRequestContract, IUpdateUnitsWorkingFormatResultContract> _nextNode;

        public LoggedUpdateUnitsWorkingFormatRequest(
            ILogger<LoggedUpdateUnitsWorkingFormatRequest> logger,
            IPipelineNode<IUpdateUnitsWorkingFormatRequestContract, IUpdateUnitsWorkingFormatResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IUpdateUnitsWorkingFormatResultContract> Ask(
            IUpdateUnitsWorkingFormatRequestContract input)
        {
            _logger.LogInformation($"UpdateUnitsWorkingFormatLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            _logger.LogInformation($"UpdateUnitsWorkingFormatLogger ends on: {DateTime.Now}");
            
            switch (result)
            {
                case IUpdateUnitsWorkingFormatSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IUpdateUnitsWorkingFormatErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}