using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.GetAllInstances
{
    public class LoggedGetAllInstancesRequest :
        PipeNodeBase<
            IGetAllInstancesRequestContract,
            IGetAllInstancesResultContract>
    {
        private readonly ILogger<LoggedGetAllInstancesRequest> _logger;
        

        public LoggedGetAllInstancesRequest(
            ILogger<LoggedGetAllInstancesRequest> logger,
            IPipeNode<IGetAllInstancesRequestContract,
                IGetAllInstancesResultContract> next) : base(next)
        {
            _logger = logger;
        }

        public override async Task<IGetAllInstancesResultContract> Ask(
            IGetAllInstancesRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetAllInstancesLogger starts on: {DateTime.Now}");
            var result = await DoNext(input, cancellationToken);
            _logger.LogInformation($"GetAllInstancesLogger ends on: {DateTime.Now}");
            
            switch (result)
            {
                case IGetAllInstancesSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IGetAllInstancesErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}