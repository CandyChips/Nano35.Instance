using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAllUnits
{
    public class LoggedGetAllUnitsRequest :
        PipeNodeBase<
            IGetAllUnitsRequestContract, 
            IGetAllUnitsResultContract>
    {
        private readonly ILogger<LoggedGetAllUnitsRequest> _logger;
        
        public LoggedGetAllUnitsRequest(
            ILogger<LoggedGetAllUnitsRequest> logger,
            IPipeNode<IGetAllUnitsRequestContract, IGetAllUnitsResultContract> next) :
            base(next)
        {
            _logger = logger;
        }


        public override async Task<IGetAllUnitsResultContract> Ask(
            IGetAllUnitsRequestContract input)
        {
            _logger.LogInformation($"GetAllUnitsLogger starts on: {DateTime.Now}");
            var result = await DoNext(input);
            _logger.LogInformation($"GetAllUnitsLogger ends on: {DateTime.Now}");
            
            switch (result)
            {
                case IGetAllUnitsSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IGetAllUnitsErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }

            return result;
        }
    }
}