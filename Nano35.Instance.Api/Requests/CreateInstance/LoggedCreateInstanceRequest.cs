using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.CreateInstance
{
    public class LoggedCreateInstanceRequest :
        PipeNodeBase<ICreateInstanceRequestContract, ICreateInstanceResultContract>
    {
        private readonly ILogger<LoggedCreateInstanceRequest> _logger;

        public LoggedCreateInstanceRequest(
            ILogger<LoggedCreateInstanceRequest> logger,
            IPipeNode<ICreateInstanceRequestContract, ICreateInstanceResultContract> next) :
            base(next)
        {
            _logger = logger;
        }

        public override async Task<ICreateInstanceResultContract> Ask(
            ICreateInstanceRequestContract input)
        {
            _logger.LogInformation($"CreateClientLogger starts on: {DateTime.Now}");
            var result = await DoNext(input);
            switch (result)
            {
                case ICreateInstanceSuccessResultContract:
                    _logger.LogInformation($"CreateInstanceLogger ends on: {DateTime.Now} with success");
                    break;
                case ICreateInstanceErrorResultContract error:
                    _logger.LogError($"CreateInstanceLogger ends on: {DateTime.Now} with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}