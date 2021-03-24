using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAllClientStates
{
    public class LoggedGetAllClientStates :
        PipeNodeBase<
            IGetAllClientStatesRequestContract,
            IGetAllClientStatesResultContract>
    {
        private readonly ILogger<LoggedGetAllClientStates> _logger;
        

        public LoggedGetAllClientStates(
            ILogger<LoggedGetAllClientStates> logger,
            IPipeNode<IGetAllClientStatesRequestContract, IGetAllClientStatesResultContract> next) :
            base(next)
        {
            _logger = logger;
        }

        public override async Task<IGetAllClientStatesResultContract> Ask(
            IGetAllClientStatesRequestContract input)
        {
            _logger.LogInformation($"GetAllClientStatesLogger starts on: {DateTime.Now}");
            var result = await DoNext(input);
            _logger.LogInformation($"GetAllClientStatesLogger ends on: {DateTime.Now}");
            
            switch (result)
            {
                case IGetAllClientStatesSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IGetAllClientStatesErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}