using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetWorkerStringById
{
    public class LoggedGetWorkerStringByIdRequest :
        IPipelineNode<IGetWorkerStringByIdRequestContract, IGetWorkerStringByIdResultContract>
    {
        private readonly ILogger<LoggedGetWorkerStringByIdRequest> _logger;
        private readonly IPipelineNode<IGetWorkerStringByIdRequestContract, IGetWorkerStringByIdResultContract> _nextNode;

        public LoggedGetWorkerStringByIdRequest(
            ILogger<LoggedGetWorkerStringByIdRequest> logger,
            IPipelineNode<IGetWorkerStringByIdRequestContract, IGetWorkerStringByIdResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetWorkerStringByIdResultContract> Ask(
            IGetWorkerStringByIdRequestContract input)
        {
            _logger.LogInformation($"GetWorkerStringByIdLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            switch (result)
            {
                case IGetWorkerStringByIdSuccessResultContract:
                    _logger.LogInformation($"GetWorkerStringByIdLogger ends on: {DateTime.Now} with success");
                    break;
                case IGetWorkerStringByIdErrorResultContract error:
                    _logger.LogError($"GetWorkerStringByIdLogger ends on: {DateTime.Now} with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}