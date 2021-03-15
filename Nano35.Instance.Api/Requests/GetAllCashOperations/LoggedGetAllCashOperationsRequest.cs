using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAllCashOperations
{
    public class LoggedGetAllCashOperationsRequest :
        PipeNodeBase<IGetAllCashOperationsRequestContract, IGetAllCashOperationsResultContract>
    {
        private readonly ILogger<LoggedGetAllCashOperationsRequest> _logger;

        public LoggedGetAllCashOperationsRequest(
            ILogger<LoggedGetAllCashOperationsRequest> logger,
            IPipeNode<IGetAllCashOperationsRequestContract, IGetAllCashOperationsResultContract> next) :
            base(next)
        {
            _logger = logger;
        }

        public override async Task<IGetAllCashOperationsResultContract> Ask(
            IGetAllCashOperationsRequestContract input)
        {
            _logger.LogInformation($"CreateClientLogger starts on: {DateTime.Now}");
            var result = await DoNext(input);
            switch (result)
            {
                case IGetAllCashOperationsSuccessResultContract:
                    _logger.LogInformation($"CreateClientLogger ends on: {DateTime.Now} with success");
                    break;
                case IGetAllCashOperationsErrorResultContract error:
                    _logger.LogError($"CreateClientLogger ends on: {DateTime.Now} with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}