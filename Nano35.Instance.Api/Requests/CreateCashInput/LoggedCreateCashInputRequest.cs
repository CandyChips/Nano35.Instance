using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.CreateCashInput
{
    public class LoggedCreateCashInputRequest :
        PipeNodeBase<ICreateCashInputRequestContract, ICreateCashInputResultContract>
    {
        private readonly ILogger<LoggedCreateCashInputRequest> _logger;

        public LoggedCreateCashInputRequest(
            ILogger<LoggedCreateCashInputRequest> logger,
            IPipeNode<ICreateCashInputRequestContract, ICreateCashInputResultContract> next) :
            base(next)
        {
            _logger = logger;
        }

        public override async Task<ICreateCashInputResultContract> Ask(
            ICreateCashInputRequestContract input)
        {
            _logger.LogInformation($"CreateCashInputLogger starts on: {DateTime.Now.ToString("dd.MM.yyyy")}");
            var result = await DoNext(input);
            switch (result)
            {
                case ICreateCashInputSuccessResultContract success:
                    _logger.LogInformation($"CreateCashInputLogger ends on: {DateTime.Now.ToString("dd.MM.yyyy")} with success");
                    break;
                case ICreateCashInputErrorResultContract error:
                    _logger.LogError($"CreateCashInputLogger ends on: {DateTime.Now.ToString("dd.MM.yyyy")} with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}