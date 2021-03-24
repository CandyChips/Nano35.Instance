using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.GetAllUnitTypes
{
    public class LoggedGetAllUnitTypesRequest :
        PipeNodeBase<
            IGetAllUnitTypesRequestContract,
            IGetAllUnitTypesResultContract>
    {
        private readonly ILogger<LoggedGetAllUnitTypesRequest> _logger;
        

        public LoggedGetAllUnitTypesRequest(
            ILogger<LoggedGetAllUnitTypesRequest> logger,
            IPipeNode<IGetAllUnitTypesRequestContract,
                IGetAllUnitTypesResultContract> next) : base(next)
        {
            _logger = logger;
        }

        public override async Task<IGetAllUnitTypesResultContract> Ask(
            IGetAllUnitTypesRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetAllUnitTypesLogger starts on: {DateTime.Now}");
            var result = await DoNext(input, cancellationToken);
            _logger.LogInformation($"GetAllUnitTypesLogger ends on: {DateTime.Now}");
            
            switch (result)
            {
                case IGetAllUnitTypesSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IGetAllUnitTypesErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}