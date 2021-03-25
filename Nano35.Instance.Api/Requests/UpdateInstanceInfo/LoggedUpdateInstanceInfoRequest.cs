using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.UpdateInstanceInfo
{
    public class LoggedUpdateInstanceInfoRequest :
        PipeNodeBase
        <IUpdateInstanceInfoRequestContract,
            IUpdateInstanceInfoResultContract>
    {
        private readonly ILogger<LoggedUpdateInstanceInfoRequest> _logger;

        public LoggedUpdateInstanceInfoRequest(
            ILogger<LoggedUpdateInstanceInfoRequest> logger,
            IPipeNode<IUpdateInstanceInfoRequestContract,
                IUpdateInstanceInfoResultContract> next) : base(next)
        {
            _logger = logger;
        }

        public override async Task<IUpdateInstanceInfoResultContract> Ask(
            IUpdateInstanceInfoRequestContract input)
        {
            _logger.LogInformation($"UpdateInstanceInfoLogger starts on: {DateTime.Now}");
            var result = await DoNext(input);
            _logger.LogInformation($"UpdateInstanceInfoLogger ends on: {DateTime.Now}");
            
            switch (result)
            {
                case IUpdateInstanceInfoSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IUpdateInstanceInfoErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}