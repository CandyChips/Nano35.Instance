using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.UpdateUnitsWorkingFormat
{
    public class LoggedUpdateUnitsWorkingFormatRequest :
        PipeNodeBase<
            IUpdateUnitsWorkingFormatRequestContract, 
            IUpdateUnitsWorkingFormatResultContract>
    {
        private readonly ILogger<LoggedUpdateUnitsWorkingFormatRequest> _logger;

        public LoggedUpdateUnitsWorkingFormatRequest(
            ILogger<LoggedUpdateUnitsWorkingFormatRequest> logger,
            IPipeNode<IUpdateUnitsWorkingFormatRequestContract,
                IUpdateUnitsWorkingFormatResultContract> next) : base(next)
        {
            _logger = logger;
        }

        public override async Task<IUpdateUnitsWorkingFormatResultContract> Ask(
            IUpdateUnitsWorkingFormatRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"LoggedUpdateUnitsWorkingFormat starts on: {DateTime.Now}");
            var result = await DoNext(input, cancellationToken);
            _logger.LogInformation($"LoggedUpdateUnitsWorkingFormat ends on: {DateTime.Now}");
            
            switch (result)
            {
                case IUpdateUnitsWorkingFormatSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IUpdateUnitsWorkingFormatErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}