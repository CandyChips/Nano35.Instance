using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetInstanceStringById
{
    public class LoggedGetInstanceStringByIdRequest :
        PipeNodeBase<IGetInstanceStringByIdRequestContract, IGetInstanceStringByIdResultContract>
    {
        private readonly ILogger<LoggedGetInstanceStringByIdRequest> _logger;

        public LoggedGetInstanceStringByIdRequest(
            ILogger<LoggedGetInstanceStringByIdRequest> logger,
            IPipeNode<IGetInstanceStringByIdRequestContract, IGetInstanceStringByIdResultContract> next) :
            base(next)
        {
            _logger = logger;
        }

        public override async Task<IGetInstanceStringByIdResultContract> Ask(
            IGetInstanceStringByIdRequestContract input)
        {
            _logger.LogInformation($"GetInstanceStringByIdLogger starts on: {DateTime.Now}");
            var result = await DoNext(input);
            switch (result)
            {
                case IGetInstanceStringByIdSuccessResultContract:
                    _logger.LogInformation($"GetInstanceStringByIdLogger ends on: {DateTime.Now} with success");
                    break;
                case IGetInstanceStringByIdErrorResultContract error:
                    _logger.LogError($"GetInstanceStringByIdLogger ends on: {DateTime.Now} with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}