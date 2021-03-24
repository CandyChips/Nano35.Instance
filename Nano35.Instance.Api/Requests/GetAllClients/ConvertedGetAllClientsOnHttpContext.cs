using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.HttpContext.instance;

namespace Nano35.Instance.Api.Requests.GetAllClients
{
    public class ConvertedGetAllClientsOnHttpContext :
        PipeInConvert
        <GetAllClientsHttpQuery, 
            IActionResult,
            IGetAllClientsRequestContract, 
            IGetAllClientsResultContract>
        {
        public ConvertedGetAllClientsOnHttpContext(IPipeNode<IGetAllClientsRequestContract, IGetAllClientsResultContract> next) : base(next) {}

        public override async Task<IActionResult> Ask(GetAllClientsHttpQuery input)
        {
            var converted = new GetAllClientsRequestContract()
            {
                InstanceId = input.InstanceId,
                ClientStateId = input.ClientStateId,
                ClientTypeId = input.ClientTypeId
            };

            var response = await DoNext(converted);
            
            return response switch
            {
                IGetAllClientsSuccessResultContract success => new OkObjectResult(success),
                IGetAllClientsErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}

