using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.UpdateUnitsType
{
    public class LoggedUpdateUnitsTypeRequest :
        PipeNodeBase
        <IUpdateUnitsTypeRequestContract,
            IUpdateUnitsTypeResultContract>
    {
        private readonly ILogger<LoggedUpdateUnitsTypeRequest> _logger;

        public LoggedUpdateUnitsTypeRequest(
            ILogger<LoggedUpdateUnitsTypeRequest> logger,
            IPipeNode<IUpdateUnitsTypeRequestContract,
                IUpdateUnitsTypeResultContract> next) : base(next)
        {
            _logger = logger;
        }

        public override async Task<IUpdateUnitsTypeResultContract> Ask(
            IUpdateUnitsTypeRequestContract input)
        {
            _logger.LogInformation($"UpdateUnitsTypeLogger starts on: {DateTime.Now}");
            var result = await DoNext(input);
            _logger.LogInformation($"UpdateUnitsTypeLogger ends on: {DateTime.Now}");
            
            switch (result)
            {
                case IUpdateUnitsTypeSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IUpdateUnitsTypeErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}