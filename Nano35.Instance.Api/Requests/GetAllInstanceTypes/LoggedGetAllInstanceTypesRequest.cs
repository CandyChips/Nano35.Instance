using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAllInstanceTypes
{
    public class LoggedGetAllInstanceTypesRequest :
        PipeNodeBase<
            IGetAllInstanceTypesRequestContract,
            IGetAllInstanceTypesResultContract>
    {
        private readonly ILogger<LoggedGetAllInstanceTypesRequest> _logger;

        public LoggedGetAllInstanceTypesRequest(
            ILogger<LoggedGetAllInstanceTypesRequest> logger,
            IPipeNode<IGetAllInstanceTypesRequestContract, IGetAllInstanceTypesResultContract> next) :
            base(next)
        {
            _logger = logger;
        }


        public override async Task<IGetAllInstanceTypesResultContract> Ask(
            IGetAllInstanceTypesRequestContract input)
        {
            _logger.LogInformation($"GetAllInstanceTypesLogger starts on: {DateTime.Now}");
            var result = await DoNext(input);
            _logger.LogInformation($"GetAllInstanceTypesLogger ends on: {DateTime.Now}");
            
            switch (result)
            {
                case IGetAllInstanceTypesSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IGetAllInstanceTypesErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}