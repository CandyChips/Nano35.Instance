using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.CreatePaymentOfComing
{
    public class LoggedCreatePaymentOfComingRequest :
        PipeNodeBase<ICreatePaymentOfComingRequestContract, ICreatePaymentOfComingResultContract>
    {
        private readonly ILogger<LoggedCreatePaymentOfComingRequest> _logger;

        public LoggedCreatePaymentOfComingRequest(
            ILogger<LoggedCreatePaymentOfComingRequest> logger,
            IPipeNode<ICreatePaymentOfComingRequestContract, ICreatePaymentOfComingResultContract> next) :
            base(next)
        {
            _logger = logger;
        }

        public override async Task<ICreatePaymentOfComingResultContract> Ask(
            ICreatePaymentOfComingRequestContract input)
        {
            _logger.LogInformation($"CreateClientLogger starts on: {DateTime.Now}");
            var result = await DoNext(input);
            switch (result)
            {
                case ICreatePaymentOfComingSuccessResultContract success:
                    _logger.LogInformation($"CreateClientLogger ends on: {DateTime.Now} with success");
                    break;
                case ICreatePaymentOfComingErrorResultContract error:
                    _logger.LogError($"CreateClientLogger ends on: {DateTime.Now} with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}