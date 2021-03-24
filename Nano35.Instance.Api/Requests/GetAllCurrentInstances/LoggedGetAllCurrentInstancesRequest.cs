using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAllCurrentInstances
{
    public class LoggedGetAllCurrentInstancesRequest :
        PipeNodeBase<IGetAllInstancesRequestContract, IGetAllInstancesResultContract>
    {
        private readonly ILogger<LoggedGetAllCurrentInstancesRequest> _logger;

        public LoggedGetAllCurrentInstancesRequest(
            ILogger<LoggedGetAllCurrentInstancesRequest> logger,
            IPipeNode<IGetAllInstancesRequestContract, IGetAllInstancesResultContract> next) :
            base(next)
        {
            _logger = logger;
        }

        public override async Task<IGetAllInstancesResultContract> Ask(
            IGetAllInstancesRequestContract input)
        {
            _logger.LogInformation($"GetAllInstancesLogger starts on: {DateTime.Now}");
            var result = await DoNext(input);
            _logger.LogInformation($"GetAllInstancesLogger ends on: {DateTime.Now}");
            
            switch (result)
            {
                case IGetAllInstancesSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IGetAllInstancesErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}