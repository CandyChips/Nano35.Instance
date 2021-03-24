using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.GetAllWorkers
{
    public class LoggedGetAllWorkersRequest :
        PipeNodeBase<
            IGetAllWorkersRequestContract,
            IGetAllWorkersResultContract>
    {
        private readonly ILogger<LoggedGetAllWorkersRequest> _logger;

        public LoggedGetAllWorkersRequest(
            ILogger<LoggedGetAllWorkersRequest> logger,
            IPipeNode<IGetAllWorkersRequestContract,
                IGetAllWorkersResultContract> next) : base(next)
        {
            _logger = logger;
        }

        public override async Task<IGetAllWorkersResultContract> Ask(
            IGetAllWorkersRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetAllWorkersLogger starts on: {DateTime.Now}");
            var result = await DoNext(input, cancellationToken);
            _logger.LogInformation($"GetAllWorkersLogger ends on: {DateTime.Now}");
            
            switch (result)
            {
                case IGetAllWorkersSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IGetAllWorkersErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}