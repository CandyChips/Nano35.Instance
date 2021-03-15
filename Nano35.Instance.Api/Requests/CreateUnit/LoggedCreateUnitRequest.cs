using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.CreateUnit
{
    public class LoggedCreateUnitRequest :
        PipeNodeBase<ICreateUnitRequestContract, ICreateUnitResultContract>
    {
        private readonly ILogger<LoggedCreateUnitRequest> _logger;

        public LoggedCreateUnitRequest(
            ILogger<LoggedCreateUnitRequest> logger,
            IPipeNode<ICreateUnitRequestContract, ICreateUnitResultContract> next) :
            base(next)
        {
            _logger = logger;
        }

        public override async Task<ICreateUnitResultContract> Ask(
            ICreateUnitRequestContract input)
        {
            _logger.LogInformation($"Create unit logger starts on: {DateTime.Now}");
            var result = await DoNext(input);
            switch (result)
            {
                case ICreateUnitSuccessResultContract:
                    _logger.LogInformation($"Create unit logger ends on: {DateTime.Now} with success");
                    break;
                case ICreateUnitErrorResultContract error:
                    _logger.LogError($"Create unit logger ends on: {DateTime.Now} with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}