using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.GetUnitById
{
    public class LoggedGetUnitByIdRequest :
        PipeNodeBase<
            IGetUnitByIdRequestContract,
            IGetUnitByIdResultContract>
    {
        private readonly ILogger<LoggedGetUnitByIdRequest> _logger;

        public LoggedGetUnitByIdRequest(
            ILogger<LoggedGetUnitByIdRequest> logger,
            IPipeNode<IGetUnitByIdRequestContract,
                IGetUnitByIdResultContract> next) : base(next)
        {
            _logger = logger;
        }

        public override async Task<IGetUnitByIdResultContract> Ask(
            IGetUnitByIdRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"LoggedGetUnitById starts on: {DateTime.Now}");
            var result = await DoNext(input, cancellationToken);
            _logger.LogInformation($"LoggedGetUnitById ends on: {DateTime.Now}");
            
            switch (result)
            {
                case IGetUnitByIdSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IGetUnitByIdErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}