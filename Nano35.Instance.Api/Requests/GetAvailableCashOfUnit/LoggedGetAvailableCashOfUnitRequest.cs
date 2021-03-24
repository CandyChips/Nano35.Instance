using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAvailableCashOfUnit
{
    public class LoggedGetAvailableCashOfUnitRequest :
        PipeNodeBase <IGetAvailableCashOfUnitRequestContract, IGetAvailableCashOfUnitResultContract>
    {
        private readonly ILogger<LoggedGetAvailableCashOfUnitRequest> _logger;

        public LoggedGetAvailableCashOfUnitRequest(
            ILogger<LoggedGetAvailableCashOfUnitRequest> logger,
            IPipeNode<IGetAvailableCashOfUnitRequestContract, IGetAvailableCashOfUnitResultContract> next) :
            base(next)
        {
            _logger = logger;
        }


        public override async Task<IGetAvailableCashOfUnitResultContract> Ask(
            IGetAvailableCashOfUnitRequestContract input)
        {
            _logger.LogInformation($"GetAvailableCashOfUnitLogger starts on: {DateTime.Now}");
            var result = await DoNext(input);
            _logger.LogInformation($"GetAvailableCashOfUnitLogger ends on: {DateTime.Now}");
            
            switch (result)
            {
                case IGetAvailableCashOfUnitSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IGetAvailableCashOfUnitErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}