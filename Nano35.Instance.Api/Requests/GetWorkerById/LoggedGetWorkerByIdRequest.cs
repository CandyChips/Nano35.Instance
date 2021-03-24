using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetWorkerById
{
    public class LoggedGetWorkerByIdRequest :
        PipeNodeBase<IGetWorkerByIdRequestContract, IGetWorkerByIdResultContract>
    {
        private readonly ILogger<LoggedGetWorkerByIdRequest> _logger;

        public LoggedGetWorkerByIdRequest(
            ILogger<LoggedGetWorkerByIdRequest> logger,
            IPipeNode<IGetWorkerByIdRequestContract, IGetWorkerByIdResultContract> next) :
            base(next)
        {
            _logger = logger;
        }

        public override async Task<IGetWorkerByIdResultContract> Ask(
            IGetWorkerByIdRequestContract input)
        {
            _logger.LogInformation($"GetWorkerByIdLogger starts on: {DateTime.Now}");
            var result = await DoNext(input);
            _logger.LogInformation($"GetWorkerByIdLogger ends on: {DateTime.Now}");
            
            switch (result)
            {
                case IGetWorkerByIdSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IGetWorkerByIdErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}