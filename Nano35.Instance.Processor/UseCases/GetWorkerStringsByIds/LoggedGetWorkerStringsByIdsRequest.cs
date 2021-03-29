using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.GetWorkerStringsByIds
{
    public class LoggedGetWorkerStringsByIdsRequest :
        PipeNodeBase<
            IGetWorkerStringsByIdsRequestContract,
            IGetWorkerStringsByIdsResultContract>
    {
        private readonly ILogger<LoggedGetWorkerStringsByIdsRequest> _logger;
        public LoggedGetWorkerStringsByIdsRequest(
            ILogger<LoggedGetWorkerStringsByIdsRequest> logger,
            IPipeNode<IGetWorkerStringsByIdsRequestContract,
                IGetWorkerStringsByIdsResultContract> next) : base(next)
        {
            _logger = logger;
        }

        public override async Task<IGetWorkerStringsByIdsResultContract> Ask(
            IGetWorkerStringsByIdsRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetWorkerStringsByIdsLogger starts on: {DateTime.Now}");
            var result = await DoNext(input, cancellationToken);
            _logger.LogInformation($"GetWorkerStringsByIdsLogger ends on: {DateTime.Now}");

            switch (result)
            {
                case IGetWorkerStringsByIdsSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IGetWorkerStringsByIdsErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}