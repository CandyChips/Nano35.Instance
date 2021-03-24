using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAllRoles
{
    public class LoggedGetAllRolesRequest :
        PipeNodeBase<
            IGetAllRolesRequestContract, 
            IGetAllRolesResultContract>
    {
        private readonly ILogger<LoggedGetAllRolesRequest> _logger;
        
        public LoggedGetAllRolesRequest(
            ILogger<LoggedGetAllRolesRequest> logger,
            IPipeNode<IGetAllRolesRequestContract, IGetAllRolesResultContract> next) :
            base(next)
        {
            _logger = logger;
        }


        public override async Task<IGetAllRolesResultContract> Ask(
            IGetAllRolesRequestContract input)
        {
            _logger.LogInformation($"GetAllRolesLogger starts on: {DateTime.Now}");
            var result = await DoNext(input);
            _logger.LogInformation($"GetAllRolesLogger ends on: {DateTime.Now}");
            
            switch (result)
            {
                case IGetAllRolesSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IGetAllRolesErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}