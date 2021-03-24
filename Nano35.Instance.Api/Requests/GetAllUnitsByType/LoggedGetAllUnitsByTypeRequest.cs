using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAllUnitsByType
{
    public class LoggedGetAllUnitsByTypeRequest :
        PipeNodeBase<IGetAllUnitsByTypeRequestContract, IGetAllUnitsByTypeResultContract>
    {
        private readonly ILogger<LoggedGetAllUnitsByTypeRequest> _logger;

        public LoggedGetAllUnitsByTypeRequest(
            ILogger<LoggedGetAllUnitsByTypeRequest> logger,
            IPipeNode<IGetAllUnitsByTypeRequestContract, IGetAllUnitsByTypeResultContract> next) :
            base(next)
        {
            _logger = logger;
        }


        public override async Task<IGetAllUnitsByTypeResultContract> Ask(
            IGetAllUnitsByTypeRequestContract input)
        {
            _logger.LogInformation($"GetAllUnitsByTypeLogger starts on: {DateTime.Now}");
            var result = await DoNext(input);
            _logger.LogInformation($"GetAllUnitsByTypeLogger ends on: {DateTime.Now}");
            
            switch (result)
            {
                case IGetAllUnitsByTypeSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IGetAllUnitsByTypeErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}