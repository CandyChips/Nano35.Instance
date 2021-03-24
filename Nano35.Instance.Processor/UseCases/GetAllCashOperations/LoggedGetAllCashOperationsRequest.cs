using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.GetAllCashOperations
{
    public class LoggedGetAllCashOperationsRequest :
        PipeNodeBase<
            IGetAllCashOperationsRequestContract, 
            IGetAllCashOperationsResultContract>
    {
        private readonly ILogger<LoggedGetAllCashOperationsRequest> _logger;

        public LoggedGetAllCashOperationsRequest(
            ILogger<LoggedGetAllCashOperationsRequest> logger,
            IPipeNode<IGetAllCashOperationsRequestContract,
                IGetAllCashOperationsResultContract> next) : base(next)
        {
            _logger = logger;
        }

        public override async Task<IGetAllCashOperationsResultContract> Ask(
            IGetAllCashOperationsRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetAllCashOperationsLogger starts on: {DateTime.Now}");
            var result = await DoNext(input, cancellationToken);
            _logger.LogInformation($"GetAllCashOperationsLogger ends on: {DateTime.Now}");
            
            switch (result)
            {
                case IGetAllCashOperationsSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IGetAllCashOperationsErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}