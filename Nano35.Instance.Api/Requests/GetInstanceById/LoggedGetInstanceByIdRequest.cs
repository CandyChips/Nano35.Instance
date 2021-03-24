using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetInstanceById
{
    public class LoggedGetInstanceByIdRequest :
        PipeNodeBase <IGetInstanceByIdRequestContract, IGetInstanceByIdResultContract>
    {
        private readonly ILogger<LoggedGetInstanceByIdRequest> _logger;

        public LoggedGetInstanceByIdRequest(
            ILogger<LoggedGetInstanceByIdRequest> logger,
            IPipeNode<IGetInstanceByIdRequestContract, IGetInstanceByIdResultContract> next) :
            base(next)
        {
            _logger = logger;
        }

        public override async Task<IGetInstanceByIdResultContract> Ask(
            IGetInstanceByIdRequestContract input)
        {
            _logger.LogInformation($"GetInstanceByIdLogger starts on: {DateTime.Now}");
            var result = await DoNext(input);
            _logger.LogInformation($"GetInstanceByIdLogger ends on: {DateTime.Now}");
            
            switch (result)
            {
                case IGetInstanceByIdSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IGetInstanceByIdErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}