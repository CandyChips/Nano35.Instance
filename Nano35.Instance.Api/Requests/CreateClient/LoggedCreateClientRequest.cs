using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.CreateClient
{
    public class LoggedCreateClientRequest :
        PipeNodeBase<ICreateClientRequestContract, ICreateClientResultContract>
    {
        private readonly ILogger<LoggedCreateClientRequest> _logger;

        public LoggedCreateClientRequest(
            ILogger<LoggedCreateClientRequest> logger,
            IPipeNode<ICreateClientRequestContract, ICreateClientResultContract> next) :
            base(next)
        {
            _logger = logger;
        }

        public override async Task<ICreateClientResultContract> Ask(
            ICreateClientRequestContract input)
        {
            _logger.LogInformation($"Create client logger starts on: {DateTime.Now}");
            var result = await DoNext(input);
            switch (result)
            {
                case ICreateClientSuccessResultContract:
                    _logger.LogInformation($"Create client logger ends on: {DateTime.Now} with success");
                    break;
                case ICreateClientErrorResultContract error:
                    _logger.LogError($"Create client logger ends on: {DateTime.Now} with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}