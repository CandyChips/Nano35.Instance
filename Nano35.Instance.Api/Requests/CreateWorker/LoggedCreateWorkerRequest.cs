using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.CreateWorker
{
    public class LoggedCreateWorkerRequest :
        PipeNodeBase<ICreateWorkerRequestContract, ICreateWorkerResultContract>
    {
        private readonly ILogger<LoggedCreateWorkerRequest> _logger;

        public LoggedCreateWorkerRequest(
            ILogger<LoggedCreateWorkerRequest> logger,
            IPipeNode<ICreateWorkerRequestContract, ICreateWorkerResultContract> next) :
            base(next)
        {
            _logger = logger;
        }

        public override async Task<ICreateWorkerResultContract> Ask(
            ICreateWorkerRequestContract input)
        {
            _logger.LogInformation($"CreateWorkerLogger starts on: {DateTime.Now}");
            var result = await DoNext(input);
            switch (result)
            {
                case ICreateWorkerSuccessResultContract:
                    _logger.LogInformation($"CreateWorkerLogger ends on: {DateTime.Now} with success");
                    break;
                case ICreateWorkerErrorResultContract error:
                    _logger.LogError($"CreateWorkerLogger ends on: {DateTime.Now} with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}