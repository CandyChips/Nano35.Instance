using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAllClients
{
    public class LoggedGetAllClientsRequest :
        PipeNodeBase<IGetAllClientsRequestContract, IGetAllClientsResultContract>
    {
        private readonly ILogger<LoggedGetAllClientsRequest> _logger;
        
        public LoggedGetAllClientsRequest(
            ILogger<LoggedGetAllClientsRequest> logger,
            IPipeNode<IGetAllClientsRequestContract, IGetAllClientsResultContract> next) :
            base(next)
        {
            _logger = logger;
        }

        public override async Task<IGetAllClientsResultContract> Ask(
            IGetAllClientsRequestContract input)
        {
            _logger.LogInformation($"Get all clients logger starts on: {DateTime.Now}");
            var result = await DoNext(input);
            switch (result)
            {
                case IGetAllClientsSuccessResultContract:
                    _logger.LogInformation($"Get all clients logger ends on: {DateTime.Now} with success");
                    break;
                case IGetAllClientsErrorResultContract error:
                    _logger.LogError($"Get all clients logger ends on: {DateTime.Now} with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}