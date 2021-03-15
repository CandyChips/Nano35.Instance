using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.CreatePaymentOfSelle
{
    public class LoggedCreatePaymentOfSelleRequest :
        PipeNodeBase<ICreatePaymentOfSelleRequestContract, ICreatePaymentOfSelleResultContract>
    {
        private readonly ILogger<LoggedCreatePaymentOfSelleRequest> _logger;

        public LoggedCreatePaymentOfSelleRequest(
            ILogger<LoggedCreatePaymentOfSelleRequest> logger,
            IPipeNode<ICreatePaymentOfSelleRequestContract, ICreatePaymentOfSelleResultContract> next) :
            base(next)
        {
            _logger = logger;
        }

        public override async Task<ICreatePaymentOfSelleResultContract> Ask(
            ICreatePaymentOfSelleRequestContract input)
        {
            _logger.LogInformation($"CreateClientLogger starts on: {DateTime.Now}");
            var result = await DoNext(input);
            switch (result)
            {
                case ICreatePaymentOfSelleSuccessResultContract:
                    _logger.LogInformation($"CreateClientLogger ends on: {DateTime.Now} with success");
                    break;
                case ICreatePaymentOfSelleErrorResultContract error:
                    _logger.LogError($"CreateClientLogger ends on: {DateTime.Now} with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}