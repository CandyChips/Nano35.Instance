using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.UpdateInstanceName
{
    public class LoggedUpdateInstanceNameRequest :
        PipeNodeBase
        <IUpdateInstanceNameRequestContract,
            IUpdateInstanceNameResultContract>
    {
        private readonly ILogger<LoggedUpdateInstanceNameRequest> _logger;

        public LoggedUpdateInstanceNameRequest(
            ILogger<LoggedUpdateInstanceNameRequest> logger,
            IPipeNode<IUpdateInstanceNameRequestContract,
                IUpdateInstanceNameResultContract> next) : base(next)
        {
            _logger = logger;
        }

        public override async Task<IUpdateInstanceNameResultContract> Ask(
            IUpdateInstanceNameRequestContract input)
        {
            _logger.LogInformation($"UpdateInstanceNameLogger starts on: {DateTime.Now}");
            var result = await DoNext(input);
            _logger.LogInformation($"UpdateInstanceNameLogger ends on: {DateTime.Now}");
            
            switch (result)
            {
                case IUpdateInstanceNameSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IUpdateInstanceNameErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}