using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.GetAllClientsTypes
{
    public class LoggedGetAllClientTypesRequest :
        PipeNodeBase<
            IGetAllClientTypesRequestContract,
            IGetAllClientTypesResultContract>
    {
        private readonly ILogger<LoggedGetAllClientTypesRequest> _logger;
        
        public LoggedGetAllClientTypesRequest(
            ILogger<LoggedGetAllClientTypesRequest> logger,
            IPipeNode<IGetAllClientTypesRequestContract,
                IGetAllClientTypesResultContract> next) : base(next)
        {
            _logger = logger;
        }

        public override async Task<IGetAllClientTypesResultContract> Ask(
            IGetAllClientTypesRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetAllClientTypesLogger starts on: {DateTime.Now}");
            var result = await DoNext(input, cancellationToken);
            _logger.LogInformation($"GetAllClientTypesLogger ends on: {DateTime.Now}");
            
            switch (result)
            {
                case IGetAllClientTypesSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IGetAllClientTypesErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}