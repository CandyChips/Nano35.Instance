using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.CreateWorker
{
    public class LoggedCreateWorkerRequest :
        PipeNodeBase<
            ICreateWorkerRequestContract,
            ICreateWorkerResultContract>
    {
        private readonly ILogger<LoggedCreateWorkerRequest> _logger;
        

        public LoggedCreateWorkerRequest(ILogger<LoggedCreateWorkerRequest> logger,
            IPipeNode<ICreateWorkerRequestContract, ICreateWorkerResultContract> next) : base(next)
        {
            _logger = logger;
        }

        public override async Task<ICreateWorkerResultContract> Ask(
            ICreateWorkerRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CreateWorkerLogger starts on: {DateTime.Now}");
            var result = await DoNext(input, cancellationToken);
            _logger.LogInformation($"CreateWorkerLogger ends on: {DateTime.Now}");
            
            switch (result)
            {
                case ICreateWorkerSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case ICreateWorkerErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}