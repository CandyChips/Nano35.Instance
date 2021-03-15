using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.CreateCashOutput
{
    public class LoggedCreateCashOutputRequest :
        PipeNodeBase<ICreateCashOutputRequestContract, ICreateCashOutputResultContract>
    {
        private readonly ILogger<LoggedCreateCashOutputRequest> _logger;

        public LoggedCreateCashOutputRequest(
            ILogger<LoggedCreateCashOutputRequest> logger,
            IPipeNode<ICreateCashOutputRequestContract, ICreateCashOutputResultContract> next) :
            base(next)
        {
            _logger = logger;
        }

        public override async Task<ICreateCashOutputResultContract> Ask(
            ICreateCashOutputRequestContract input)
        {
            _logger.LogInformation($"Create cash output logger starts on: {DateTime.Now.ToString("dd.MM.yyyy")}");
            var result = await DoNext(input);
            switch (result)
            {
                case ICreateCashOutputSuccessResultContract success:
                    _logger.LogInformation($"Create cash output logger ends on: {DateTime.Now.ToString("dd.MM.yyyy")} with success");
                    break;
                case ICreateCashOutputErrorResultContract error:
                    _logger.LogError($"Create cash output logger ends on: {DateTime.Now.ToString("dd.MM.yyyy")} with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}