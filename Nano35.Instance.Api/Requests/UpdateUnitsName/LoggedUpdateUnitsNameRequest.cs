using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.UpdateUnitsName
{
    public class LoggedUpdateUnitsNameRequest :
        PipeNodeBase
        <IUpdateUnitsNameRequestContract,
            IUpdateUnitsNameResultContract>
    {
        private readonly ILogger<LoggedUpdateUnitsNameRequest> _logger;

        public LoggedUpdateUnitsNameRequest(
            ILogger<LoggedUpdateUnitsNameRequest> logger,
            IPipeNode<IUpdateUnitsNameRequestContract,
                IUpdateUnitsNameResultContract> next) : base(next)
        {
            _logger = logger;
        }

        public override async Task<IUpdateUnitsNameResultContract> Ask(
            IUpdateUnitsNameRequestContract input)
        {
            _logger.LogInformation($"UpdateUnitsNameLogger starts on: {DateTime.Now}");
            var result = await DoNext(input);
            _logger.LogInformation($"UpdateUnitsNameLogger ends on: {DateTime.Now}");
            
            switch (result)
            {
                case IUpdateUnitsNameSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IUpdateUnitsNameErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}